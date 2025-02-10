using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Models;

namespace CafeManagement.Interfaces.Mappers
{
    public interface ICategoryMapper
    {
        CategoryResponse MapToResponse(Category category);
        Category MapToEntity(CategoryRequest request);
        void UpdateEntityFromRequest(Category category, CategoryRequest request);
    }
}
