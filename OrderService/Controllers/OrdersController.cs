using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using SagaPattern.Core.Models;
using SagaPattern.Core.Models.Entities;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController(OrderDbContext context) : ControllerBase
{
    [HttpGet]
    [Route("api/order")]
    public async Task<IEnumerable<Order>> GetOrders()
    {
        
        return await Task.FromResult(context.Orders.ToList());
    }

    [HttpGet]
    [Route("api/order/{id}")]
    public async Task<Order?> GetOrderById(int id)
    {
        return await context.Orders.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    [HttpPost]
    [Route("api/order")]
    public async Task<object> CreateOrder([FromBody] Order order)
    {
        order.Status = "Created";
        context.Orders.Add(order);
        return await Task.FromResult(new { orderId = order.Id, status = order.Status});
    }

    [HttpDelete]
    [Route("api/order")]
    public async Task<bool> CancelOrder(int id)
    {
        var order = context.Orders.FirstOrDefault(x => x.Id == id);
        if (order == null)
        {
            return false;
        }
        context.Orders.Remove(order);
        return true;
    }
}