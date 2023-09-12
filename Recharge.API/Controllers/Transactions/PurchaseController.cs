using Microsoft.AspNetCore.Mvc;
using Recharge.Application.DTOs.Transactions;
using Recharge.Application.Interfaces.Transactions;

namespace Recharge.API.Controllers.Transactions;

[Route("Purchase")]
[ApiController]
public class PurchaseController : ControllerBase {
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService) {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<ActionResult> CreatePurchase([FromBody] PurchaseDTO purchaseDTO) {
        var result = await _purchaseService.CreatePurchase(purchaseDTO);

        if (result != null) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetPurchaseById(Guid id) {
        var result = await _purchaseService.GetPurchaseById(id);

        if (result != null) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult> GetPurchasesByUserId(Guid userId) {
        var result = await _purchaseService.GetPurchasesByUserId(userId);

        if (result != null) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllPurchases() {
        var result = await _purchaseService.GetAllPurchases();

        if (result != null) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePurchase(Guid id, [FromBody] PurchaseDTO purchaseDTO) {
        var result = await _purchaseService.UpdatePurchase(id, purchaseDTO);

        if (result != null) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemovePurchase(Guid id) {
        var result = await _purchaseService.RemovePurchase(id, null);

        if (result != null) {
            return Ok(result);
        }

        return NotFound(result);
    }
}