using SagaOrchestrator.Api.Models.Saga.Framework;
using SagaPattern.Core.Models;
using SagaPattern.Core.Models.Entities;
using SagaPattern.Core.Models.Saga;

namespace SagaOrchestrator.Api.Models.Saga.Activities;

public class ReserveInventoryActivity(Order order) : IActivity
{
    public async Task<ActivityStatus> ExecuteAsync()
    {
        Console.WriteLine($"Reserve Inventory Activity on order {order.Id}");
        return await Task.FromResult(ActivityStatus.Succeeded);
    }

    public async Task<ActivityStatus> CompensateAsync()
    {
        var releaseInventoryRequest = new ReleaseInventoryRequest
        {
            productId = order.ProductId,
            quantity = order.Quantity
        };
        Console.WriteLine($"Release Inventory Activity on order {order.Id} with product id {releaseInventoryRequest.productId} and quantity {releaseInventoryRequest.quantity}");
        return await Task.FromResult(ActivityStatus.Succeeded);
    }
}