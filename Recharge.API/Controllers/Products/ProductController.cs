using Microsoft.AspNetCore.Mvc;
using Recharge.Application.DTOs.Products;
using Recharge.Application.Interfaces.Products;

namespace Recharge.API.Controllers.Products {

    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductDTO productDTO) {
            var result = await _productService.CreateProduct(productDTO);

            if (result.isSucess) {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(Guid id) {
            var result = await _productService.GetProductById(id);

            if (result.isSucess) {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet("name/{productName}")]
        public async Task<ActionResult> GetProductByName(string productName) {
            var result = await _productService.GetProductByName(productName);

            if (result.isSucess) {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet("sku/{sku}")]
        public async Task<ActionResult> GetProductBySku(string sku) {
            var result = await _productService.GetProductBySku(sku);

            if (result.isSucess) {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet("barcode/{barCode}")]
        public async Task<ActionResult> GetProductByBarCode(string barCode) {
            var result = await _productService.GetProductByBarCode(barCode);

            if (result.isSucess) {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts() {
            var result = await _productService.GetAllProducts();

            if (result.isSucess) {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult> GetAllProductsInTheCategory(Guid categoryId) {
            var result = await _productService.GetAllProductsInTheCategory(categoryId);

            if (result.isSucess) {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("brand/{brandId}")]
        public async Task<ActionResult> GetAllProductsInTheBrand(Guid brandId) {
            var result = await _productService.GetAllProductsInTheBrand(brandId);

            if (result.isSucess) {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(Guid id, [FromBody] ProductDTO productDTO) {
            var result = await _productService.UpdateProduct(id, productDTO);

            if (result.isSucess) {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveProduct(Guid id) {
            var result = await _productService.RemoveProduct(id);

            if (result.isSucess) {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}