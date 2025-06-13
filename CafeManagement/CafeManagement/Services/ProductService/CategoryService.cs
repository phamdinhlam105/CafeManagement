﻿using CafeManagement.Interfaces.Services.ProductService;
using CafeManagement.Models.ProductModel;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.ProductService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> Add(Category item)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }
            await _unitOfWork.Category.Add(item);
            return item;
        }

        public async Task Delete(Category item)
        {
            if (item != null)
                await _unitOfWork.Category.Delete(item);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _unitOfWork.Category.GetAll();
        }

        public async Task<Category> GetById(Guid id)
        {
            return await _unitOfWork.Category.GetById(id);
        }
        public async Task<IEnumerable<Product>> GetProductsByCategory(Guid categoryId)
        {
            var category = await _unitOfWork.Category.GetById(categoryId);
            if (category == null)
                throw new Exception("Category id not found");
            return await _unitOfWork.Product.GetByCategoryId(categoryId);
        }

        public async Task<Category> Update(Category item)
        {
            if (item != null)
                await _unitOfWork.Category.Update(item);
            return item;
        }
    }
}
