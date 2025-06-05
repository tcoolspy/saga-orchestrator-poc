namespace SagaPattern.Core.Models.Saga;

public class SagaExecutionContext
{
    // TODO: add ability to persist saga execution info for recovery/retries
    public Guid SagaId { get; set; }
    public IList<IActivity> Activities { get; set; }
    public SagaStatus Status { get; set; }
    public int CurrentActivity { get; set; }
    public int LastActivity { get; set; }
}