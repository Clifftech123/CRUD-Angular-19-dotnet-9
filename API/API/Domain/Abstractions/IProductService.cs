using API.Domain.Contract;

namespace API.Domain.Abstractions
{
    public interface IProductService
    {
        Task<ApiResponse<ProductResponse>> CreateProductAsync(CreateProductRequest request);
        Task<ApiResponse<ProductResponse>> UpdateProductAsync(Guid id, UpdateProductRequest request);
        Task<ApiResponse<ProductResponse>> GetProductByIdAsync(Guid id);
        Task<ApiResponse<List<ProductResponse>>> GetAllProductsAsync();
        Task<ApiResponse<bool>> DeleteProductAsync(Guid id);

    }
}
