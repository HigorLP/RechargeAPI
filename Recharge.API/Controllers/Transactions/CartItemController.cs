using Microsoft.AspNetCore.Mvc;
using Recharge.Application.DTOs.Transactions;
using Recharge.Application.Interfaces.Transactions;

namespace Recharge.API.Controllers.Transactions {
    [Route("Cart")]
    [ApiController]
    public class CartItemController : ControllerBase {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService) {
            _cartItemService = cartItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItemDTO cartItemDTO) {
            var result = await _cartItemService.CreateCartItem(cartItemDTO);

            if (result.isSucess) {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItemById(Guid id) {
            var result = await _cartItemService.GetCartItemById(id);

            if (result.isSucess) {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet("purchase/{purchaseId}")]
        public async Task<IActionResult> GetCartItensByPurchase(Guid purchaseId) {
            var result = await _cartItemService.GetCartItensByPurchase(purchaseId);

            if (result.isSucess) {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCartItem(Guid id) {
            var result = await _cartItemService.RemoveCartItem(id, null);

            if (result.isSucess) {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}