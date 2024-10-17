using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Category item)
        {
            _unitOfWork.Category.Add(item);
        }

        public void Delete(Category item)
        {
            if (item != null)
                _unitOfWork.Category.Delete(item);
        }

        public IEnumerable<Category> GetAll()
        {
            return _unitOfWork.Category.GetAll();
        }

        public Category GetById(Guid id)
        {
            return _unitOfWork.Category.GetById(id);
        }

        public void Update(Category item)
        {
            if (item != null)
                _unitOfWork.Category.Update(item);
        }
    }
}
