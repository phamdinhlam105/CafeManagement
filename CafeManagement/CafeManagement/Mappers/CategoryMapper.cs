using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models;

namespace CafeManagement.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        private readonly IProductMapper _productMapper;
        public CategoryMapper(IProductMapper productMapper)
        {
            _productMapper = productMapper;
        }
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
                Name = category.Name,
                Description=category.Description,
                Products = category.Products != null
            ? category.Products.Select(p => _productMapper.MapToResponse(p)).ToList()
            : new List<ProductResponse>()
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
