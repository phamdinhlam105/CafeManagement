using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Response.ProductRes;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.ProductModel;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IProductMapper : IRequestToEntity<ProductRequest, Product>,
        IEntityToResponse<Product, ProductResponse>,
        IRequestToUpdate<ProductRequest, Product>
    {
    }
}
