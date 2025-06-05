namespace SagaPattern.Core.Models.Saga;

public enum SagaStatus
{
    NotStarted,
    Running,
    Succeeded,
    Failed,
    UnexpectedError
}