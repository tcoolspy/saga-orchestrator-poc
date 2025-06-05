namespace SagaPattern.Core.Models.Saga;

public interface IActivity
{
    Task<ActivityStatus> ExecuteAsync();
    Task<ActivityStatus> CompensateAsync();
}