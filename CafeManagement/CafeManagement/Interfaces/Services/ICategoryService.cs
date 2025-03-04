using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services
{
    public interface ICategoryService:IService<Category>
    {
        Task<IEnumerable<Product>> GetProductsByCategory(Guid categoryId);
    }
}
