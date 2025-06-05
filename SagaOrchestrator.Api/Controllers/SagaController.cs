using Microsoft.AspNetCore.Mvc;
using SagaOrchestrator.Api.Models.Saga;
using SagaOrchestrator.Api.Models.Saga.Activities;
using SagaPattern.Core.Models;
using SagaPattern.Core.Models.Entities;
using SagaPattern.Core.Models.Saga;

namespace SagaOrchestrator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SagaController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    [HttpPost]
    [Route("api/saga")]
    public async Task<IActionResult> ProcessOrder([FromBody] Order order)
    {
        // step 1: create order
        order.Status = "Pending";
        var orderResponse = await CreateOrder(order);
        if (!orderResponse.IsSuccessStatusCode)
        {
            return BadRequest("Order Creation Failed");
        }
        
        // step 2: reserve inventory
        var inventoryResponse = await ReserveInventory(order);
        if (!inventoryResponse.IsSuccessStatusCode)
        {
            await CancelOrder(order.Id);
            return BadRequest("Inventory Reservation Failed");
        }
        
        // step 3: process payment
        var paymentResponse = await ProcessPayment(order);
        if (!paymentResponse.IsSuccessStatusCode)
        {
            await ReleaseInventory(order.ProductId, order.Quantity);
            await CancelOrder(order.Id);
            return BadRequest("Payment Processing Failed"); 
        }
        
        order.Status = "Completed";

        return Ok(order);
    }

    [HttpPost]
    [Route("api/saga/process")]
    public async Task<IActionResult> ProcessOrderNew([FromBody] Order order)
    {
        var orderSaga = new SagaPattern.Core.Models.Saga.SagaOrchestrator();
        orderSaga.AddActivity(new CreateOrderActivity(order));
        orderSaga.AddActivity(new ReserveInventoryActivity(order));
        orderSaga.AddActivity(new ProcessPaymentActivity(order));
        var cancellationTokenSource = new CancellationTokenSource();
        var status = await orderSaga.Run(cancellationTokenSource.Token);
        return Ok(status.ToString());
    }

    private async Task<HttpResponseMessage> CreateOrder(Order order)
    {
        var client = httpClientFactory.CreateClient("OrderService");
        return await client.PostAsJsonAsync("api/order", order); // substitute with order api url
    }
    
    private async Task<HttpResponseMessage> ReserveInventory(Order order)
    {
        var client = httpClientFactory.CreateClient("InventoryService");
        return await client.PostAsJsonAsync("api/inventory", order); // substitute with inventory api url
    }

    private async Task<HttpResponseMessage> ProcessPayment(Order order)
    {
        var client = httpClientFactory.CreateClient("PaymentService");
        return await client.PostAsJsonAsync("api/payment", order); // substitute with payment api url
    }
    
    private async Task<HttpResponseMessage> CancelOrder(int orderId)
    {
        var client = httpClientFactory.CreateClient("OrderService");
        return await client.DeleteAsync($"api/order/{orderId}"); // substitute with order api url
    }
    
    private async Task<HttpResponseMessage> ReleaseInventory(string productId, int quantity)
    {
        var client = httpClientFactory.CreateClient("InventoryService");
        return await client.PostAsJsonAsync($"api/inventory/release", new {productId, quantity}); // substitute with inventory api url
    }   
}