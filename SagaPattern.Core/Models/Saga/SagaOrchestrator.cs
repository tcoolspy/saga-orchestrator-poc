namespace SagaPattern.Core.Models.Saga;

public class SagaOrchestrator
{
    protected SagaExecutionContext Context { get; set; }

    public SagaOrchestrator()
    {
        Context = new SagaExecutionContext();
        Context.SagaId = Guid.NewGuid();
        Context.Status = SagaStatus.NotStarted;
        Context.Activities = new List<IActivity>();
    }

    public void AddActivity(IActivity activity)
    {
        Context.Activities.Add(activity);
    }

    public IList<IActivity> GetActivities()
    {
        return Context.Activities;
    }

    public SagaStatus Status
    {
        get => Context.Status;
    }

    public int CurrentActivity
    {
        get => Context.CurrentActivity;
    }

    public async Task<SagaStatus> Run(CancellationToken cancellationToken)
    {
        if (Context.Activities!.Count == 0)
        {
            return Context.Status;
        }
        
        Context.Status = SagaStatus.Running;
        ActivityStatus activityStatus;
        
        activityStatus = await ExecutingActivities(cancellationToken);

        if (activityStatus == ActivityStatus.Succeeded)
        {
            Context.Status = SagaStatus.Succeeded;
            return Context.Status;
        }

        if (Context.CurrentActivity == 0)
        {
            Context.Status = SagaStatus.Failed;
            return Context.Status;
        }

        activityStatus = await CompensatingActivities();

        if (activityStatus == ActivityStatus.Failed)
        {
            Context.Status = SagaStatus.UnexpectedError;
        }
        
        return Context.Status;
    }

    private async Task<ActivityStatus> ExecutingActivities(CancellationToken cancellationToken)
    {
        ActivityStatus activityStatus = ActivityStatus.Failed;
        IActivity activity;

        for (Context.CurrentActivity = 0;
             Context.CurrentActivity <
             Context.Activities!.Count;
             ++Context.CurrentActivity)
        {
            Context.LastActivity = Context.CurrentActivity;
            
            activity = Context.Activities[Context.CurrentActivity];

            try
            {
                activityStatus = await activity.ExecuteAsync();
            }
            catch (Exception e)
            {
                activityStatus = ActivityStatus.Failed;
            }

            if (cancellationToken.IsCancellationRequested) break;

            if (activityStatus == ActivityStatus.Failed) break;
        }
        
        return activityStatus;
    }

    private async Task<ActivityStatus> CompensatingActivities()
    {
        ActivityStatus activityStatus = ActivityStatus.Succeeded;
        
        IActivity activity;
        
        --Context.CurrentActivity;

        Context.Status = SagaStatus.Failed;

        for (; Context.CurrentActivity >= 0; --Context.CurrentActivity)
        {
            activity = Context.Activities![Context.CurrentActivity];
            try
            {
                if (await activity.CompensateAsync() != ActivityStatus.Succeeded)
                {
                    activityStatus = ActivityStatus.Failed;
                }
            }
            catch (Exception e)
            {
                activityStatus = ActivityStatus.Failed;
            }
        }
        
        return activityStatus;
    }
}