using API.Domain.Models;

namespace API.Domain.Contract
{

    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        // Static method to map from Product to ProductResponse
        public static ProductResponse FromProduct(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
        }
    }

}
