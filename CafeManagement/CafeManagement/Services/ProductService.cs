using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(Product item)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }
            await _unitOfWork.Product.Add(item);
        }

        public async Task Delete(Product item)
        {
            if(item!=null)
                await _unitOfWork.Product.Delete(item);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _unitOfWork.Product.GetAll();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _unitOfWork.Product.GetById(id);
        }


        public async Task Update(Product item)
        {
            if (item != null)
                await _unitOfWork.Product.Update(item);
        }
    }
}
