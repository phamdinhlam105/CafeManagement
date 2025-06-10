using CafeManagement.Interfaces.Services.ProductService;
using CafeManagement.Models.ProductModel;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Product> Add(Product item)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }
            await _unitOfWork.Product.Add(item);
            return item;
        }

        public async Task Delete(Product item)
        {
            if (item != null)
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


        public async Task<Product> Update(Product item)
        {
            if (item != null)
                await _unitOfWork.Product.Update(item);
            return item;
        }

    }
}
