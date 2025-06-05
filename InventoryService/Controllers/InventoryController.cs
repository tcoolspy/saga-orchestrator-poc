using InventoryService.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController(InventoryDbContext context) : ControllerBase
{
    [HttpPost]
    [Route("api/inventory/reserve")]
    public IActionResult ReserveInventory([FromBody] dynamic request)
    {
        int productId = request.productId;
        int quantity = request.quantity;
        var inventory = context.Inventories.FirstOrDefault(x => x.ProductId == productId);
        if (inventory == null || inventory.Stock < quantity)
        {
            return BadRequest("Insufficient stock");
        }
        inventory.Stock -= quantity;
        return Ok();
    }

    [HttpPost]
    [Route("api/inventory/release")]
    public IActionResult ReleaseInventory([FromBody] dynamic request)
    {
        int productId = request.productId;
        int quantity = request.quantity;
        var inventory = context.Inventories.FirstOrDefault(x => x.ProductId == productId);
        if (inventory == null)
        {
            return NotFound("Inventory not found");
        }

        inventory.Stock += quantity;
        return Ok();
    }
}