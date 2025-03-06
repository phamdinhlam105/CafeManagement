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
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = (request.Description==null) ? string.Empty:request.Description            };
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
            if (request.Description != null)
                category.Description = request.Description;
        }
    }
}
