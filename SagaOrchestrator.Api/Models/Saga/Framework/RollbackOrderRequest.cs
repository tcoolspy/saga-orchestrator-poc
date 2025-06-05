namespace SagaOrchestrator.Api.Models.Saga.Framework;

public class RollbackOrderRequest
{
    public int OrderId { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}