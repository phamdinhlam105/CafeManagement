using CafeManagement.Models.ProductModel;

namespace CafeManagement.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryId(Guid IdCategory);
    }
}
