using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services
{
    public interface IProductService:IService<Product>
    {
        IEnumerable<Product> GetProductsByCategory(Guid categoryId);
    }
}
