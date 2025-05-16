using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IProductMapper : IRequestToEntity<ProductRequest, Product>,
        IEntityToResponse<Product, ProductResponse>,
        IRequestToUpdate<ProductRequest, Product>
    {
    }
}
