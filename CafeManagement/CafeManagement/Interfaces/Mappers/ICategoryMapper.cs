using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models;

namespace CafeManagement.Interfaces.Mappers
{
    public interface ICategoryMapper : IRequestToEntity<CategoryRequest, Category>,
        IEntityToResponse<Category, CategoryResponse>,
        IRequestToUpdate<CategoryRequest, Category>
    {

    }
}
