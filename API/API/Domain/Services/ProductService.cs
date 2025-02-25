using API.Domain.Abstractions;
using API.Domain.Contract;
using API.Infrastructure;
using API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using static API.Infrastructure.Exceptions.CustomExceptions;

namespace API.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<ProductResponse>> CreateProductAsync(CreateProductRequest request)
        {
            _logger.LogInformation("Creating new product with name: {Name}", request.Name);

            if (request.Price <= 0)
            {
                throw new InvalidProductPriceException(request.Price);
            }

            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Name == request.Name);

            if (existingProduct != null)
            {
                throw new DuplicateProductNameException(request.Name);
            }

            var product = request.ToModel();

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product created successfully with ID: {Id}", product.Id);

            return new ApiResponse<ProductResponse>
            {
                Data = product.ToResponse(),
                Message = "Product created successfully",
                Success = true,
                StatusCode = StatusCodes.Status201Created
            };
        }

        public async Task<ApiResponse<ProductResponse>> GetProductByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching product with ID: {Id}", id);

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }

            return new ApiResponse<ProductResponse>
            {
                Data = product.ToResponse(),
                Message = "Product retrieved successfully",
                Success = true,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<ApiResponse<List<ProductResponse>>> GetAllProductsAsync()
        {
            _logger.LogInformation("Fetching all products");

            var products = await _context.Products
                .Select(p => p.ToResponse())
                .ToListAsync();

            return new ApiResponse<List<ProductResponse>>
            {
                Data = products,
                Message = "Products retrieved successfully",
                Success = true,
                TotalCount = products.Count,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<ApiResponse<ProductResponse>> UpdateProductAsync(Guid id, UpdateProductRequest request)
        {
            _logger.LogInformation("Updating product with ID: {Id}", id);

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }

            if (request.Price > 0)
            {
                product.Price = request.Price;
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id != id);

                if (existingProduct != null)
                {
                    throw new DuplicateProductNameException(request.Name);
                }
                product.Name = request.Name;
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                product.Description = request.Description;
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Product updated successfully with ID: {Id}", id);

            return new ApiResponse<ProductResponse>
            {
                Data = product.ToResponse(),
                Message = "Product updated successfully",
                Success = true,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<ApiResponse<bool>> DeleteProductAsync(Guid id)
        {
            _logger.LogInformation("Deleting product with ID: {Id}", id);

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product deleted successfully with ID: {Id}", id);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Product deleted successfully",
                Success = true,
                StatusCode = StatusCodes.Status204NoContent
            };
        }
    }
}
