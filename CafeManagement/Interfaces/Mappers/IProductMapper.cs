using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Models;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IProductMapper
    {
        ProductResponse MapToResponse(Product product);
        Product MapToEntity(ProductRequest request);
        void UpdateEntityFromRequest(Product product, ProductRequest request);
    }
}
