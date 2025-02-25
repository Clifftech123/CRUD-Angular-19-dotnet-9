using API.Domain.Abstractions;
using API.Domain.Contract;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            _logger.LogInformation("Getting all products");
            var result = await _productService.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            _logger.LogInformation("Getting product with ID: {Id}", id);
            var result = await _productService.GetProductByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            _logger.LogInformation("Creating new product");
            var result = await _productService.CreateProductAsync(request);
            return CreatedAtAction(nameof(GetProductById), new { id = result.Data.Id }, result);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request)
        {
            _logger.LogInformation("Updating product with ID: {Id}", id);
            var result = await _productService.UpdateProductAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            _logger.LogInformation("Deleting product with ID: {Id}", id);
            var result = await _productService.DeleteProductAsync(id);
            return Ok(result);
        }
    }
}
