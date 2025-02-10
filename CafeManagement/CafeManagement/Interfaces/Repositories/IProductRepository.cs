using CafeManagement.Models;

namespace CafeManagement.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetByCategoryId(Guid IdCategory);
    }
}
