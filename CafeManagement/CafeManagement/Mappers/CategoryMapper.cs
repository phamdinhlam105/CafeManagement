using CafeManagement.Dtos.Request.ProductReq;
using CafeManagement.Dtos.Response.ProductRes;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.ProductModel;

namespace CafeManagement.Mappers
{
    public class CategoryMapper :ICategoryMapper
    {
        public Category MapToEntity(CategoryRequest request)
        {
            return new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description ?? string.Empty
            };
        }

        public CategoryResponse MapToResponse(Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description=category.Description,
                NumberOfProducts =category.Products.Count
            };
        }

        public void UpdateEntityFromRequest(CategoryRequest request, Category category)
        {
            category.Name = request.Name;
            if (request.Description != null)
                category.Description = request.Description;
        }
    }
}
