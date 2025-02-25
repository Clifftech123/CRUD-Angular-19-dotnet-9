using API.Domain.Contract;
using API.Domain.Models;

namespace API.Infrastructure
{
    public static class Mapping
    {
        public static ProductResponse ToResponse(this Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
        }

        public static Product ToModel(this CreateProductRequest request)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                Description = request.Description
            };
        }

        public static void UpdateFromRequest(this Product product, UpdateProductRequest request)
        {
            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
        }
    }
}
