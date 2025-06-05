namespace SagaOrchestrator.Api.Models.Saga.Framework;

public class ReleaseInventoryRequest
{
    public string productId { get; set; }
    public int quantity { get; set; }
}