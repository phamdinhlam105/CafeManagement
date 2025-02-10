using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models;

namespace CafeManagement.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public Category MapToEntity(CategoryRequest request)
        {
            return new Category
            {
                Id = new Guid(),
                Name = request.Name
            };
        }

        public CategoryResponse MapToResponse(Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public void UpdateEntityFromRequest(Category category, CategoryRequest request)
        {
            category.Name= request.Name;
        }
    }
}
