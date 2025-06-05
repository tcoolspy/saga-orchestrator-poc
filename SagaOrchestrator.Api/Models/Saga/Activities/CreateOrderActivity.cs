using SagaPattern.Core.Models;
using SagaPattern.Core.Models.Entities;
using SagaPattern.Core.Models.Saga;

namespace SagaOrchestrator.Api.Models.Saga.Activities;

public class CreateOrderActivity(Order order) : IActivity
{
    public async Task<ActivityStatus> ExecuteAsync()
    {
        Console.WriteLine($"Create Order Activity on order {order.Id}");
        return await Task.FromResult(ActivityStatus.Succeeded);
    }

    public async Task<ActivityStatus> CompensateAsync()
    {
        Console.WriteLine($"Compensate Order Activity on order {order.Id}");
        return await Task.FromResult(ActivityStatus.Succeeded);
    }
}