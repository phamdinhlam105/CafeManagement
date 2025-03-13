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
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                Img= (request.Img==null)?string.Empty:request.Img,
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
                Category = new CategoryResponse
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                }
            };
        }

        public void UpdateEntityFromRequest(Product product, ProductRequest request)
        {
            product.Name = request.Name;
            product.Price = request.Price;
            product.Img = (request.Img == null) ? string.Empty : request.Img;
            product.CategoryId = request.CategoryId;
        }
    }
}
