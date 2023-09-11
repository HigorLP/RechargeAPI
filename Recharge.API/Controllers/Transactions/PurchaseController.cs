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
    public async Task<IActionResult> CreatePurchase([FromBody] PurchaseDTO purchaseDTO) {
        var result = await _purchaseService.CreatePurchase(purchaseDTO);

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPurchaseById(Guid id) {
        var result = await _purchaseService.GetPurchaseById(id);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetPurchasesByUserId(Guid userId) {
        var result = await _purchaseService.GetPurchasesByUserId(userId);

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPurchases() {
        var result = await _purchaseService.GetAllPurchases();

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePurchase(Guid id, [FromBody] PurchaseDTO purchaseDTO) {
        var result = await _purchaseService.UpdatePurchase(id, purchaseDTO);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemovePurchase(Guid id) {
        var result = await _purchaseService.RemovePurchase(id, null);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }
}