using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public void Add(Product item)
        {
            _unitOfWork.Product.Add(item);
        }

        public void Delete(Guid id)
        {
            Product item = _unitOfWork.Product.GetById(id);
            if(item!=null)
                _unitOfWork.Product.Delete(item);
        }

        public IEnumerable<Product> GetAll()
        {
            return _unitOfWork.Product.GetAll();
        }

        public Product GetById(Guid id)
        {
            return _unitOfWork.Product.GetById(id);
        }

        public IEnumerable<Product> GetProductsByCategory(Guid categoryId)
        {
            return _unitOfWork.Product.GetByCategoryId(categoryId);
        }

        public void Update(Product item)
        {
            if (item != null)
                _unitOfWork.Product.Update(item);
        }
    }
}
