using CafeManagement.Models.ProductModel;

namespace CafeManagement.Interfaces.Services.ProductService
{
    public interface ICategoryService : IService<Category>
    {
        Task<IEnumerable<Product>> GetProductsByCategory(Guid categoryId);
    }
}
