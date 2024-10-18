using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models;

namespace CafeManagement.Mappers
{
    public class ProductMapper : IProductMapper
    {
        public Product MapToEntity(ProductRequest request)
        {
            return new Product
            {
                Id = new Guid(),
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId,
            };
        }

        public ProductResponse MapToResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
            };
        }

        public void UpdateEntityFromRequest(Product product, ProductRequest request)
        {
            product.Name = request.Name;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;
        }
    }
}
