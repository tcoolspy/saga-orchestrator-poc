using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using SagaPattern.Core.Models;
using SagaPattern.Core.Models.Entities;

namespace PaymentService.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController(PaymentDbContext context) : ControllerBase
{
    [HttpPost]
    [Route("api/payment")]
    public IActionResult ProcessPayment([FromBody] dynamic request)
    {
        int orderId = request.orderId;
        decimal amount = request.amount;
        var payment = new Payment
        {
            OrderId = orderId,
            Amount = amount,
            Status = "Processed"
        };
        context.Payments.Add(payment);
        context.SaveChanges();
        return Ok(payment);
    }
}