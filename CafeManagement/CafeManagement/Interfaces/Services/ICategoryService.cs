using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services
{
    public interface ICategoryService:IService<Category>
    {
        public IEnumerable<Product> GetProductsByCategory(Guid categoryId);
    }
}
