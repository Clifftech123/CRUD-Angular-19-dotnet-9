namespace API.Infrastructure.Exceptions
{
    public class CustomExceptions
    {
        public class ProductNotFoundException : Exception
        {
            public ProductNotFoundException(Guid id)
                : base($"Product with ID {id} was not found")
            {
            }
        }

        public class InvalidProductPriceException : Exception
        {
            public InvalidProductPriceException(decimal price)
                : base($"Invalid product price: {price}. Price must be greater than 0")
            {
            }
        }

        public class DuplicateProductNameException : Exception
        {
            public DuplicateProductNameException(string name)
                : base($"Product with name '{name}' already exists")
            {
            }
        }

        public class InvalidProductDataException : Exception
        {
            public InvalidProductDataException(string message)
                : base($"Invalid product data: {message}")
            {
            }
        }
    }
}
