using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.ProductModel;

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
                Description=request.Description,
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
                CategoryName = product.Category.Name,
                Description = product.Description,
                Img=product.Img
            };
        }

        public void UpdateEntityFromRequest(ProductRequest request, Product product)
        {
            product.Name = request.Name;
            product.Price = request.Price;
            product.Img = request.Img ?? string.Empty;
            product.Description= request.Description ?? string.Empty;
            product.CategoryId = request.CategoryId;
        }
    }
}
