using SagaOrchestrator.Api.Models.Saga.Framework;
using SagaPattern.Core.Models;
using SagaPattern.Core.Models.Entities;
using SagaPattern.Core.Models.Saga;

namespace SagaOrchestrator.Api.Models.Saga.Activities;

public class ProcessPaymentActivity(Order order) : IActivity
{
    public async Task<ActivityStatus> ExecuteAsync()
    {
        Console.WriteLine($"Process Payment Activity on order {order.Id}");
        Random random = new Random();
        int randomNumber = random.Next(0, 2);
        switch (randomNumber)
        {
            case 0:
                return await Task.FromResult(ActivityStatus.Failed);
                break;
            case 1:
                return await Task.FromResult(ActivityStatus.Succeeded);
                break;
            default:
                return await Task.FromResult(ActivityStatus.Succeeded);
        }
    }

    public async Task<ActivityStatus> CompensateAsync()
    {
        var rollbackOrderRequest = new RollbackOrderRequest
        {
            OrderId = order.Id,
            ProductId = order.ProductId,
            Quantity = order.Quantity,
        };
        Console.WriteLine(
            $"Rollback Payment Activity on order {rollbackOrderRequest.OrderId} for productId {rollbackOrderRequest.ProductId} with quantity {rollbackOrderRequest.Quantity}");
        return await Task.FromResult(ActivityStatus.Succeeded);
    }
}