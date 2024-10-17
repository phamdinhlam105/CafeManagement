using CafeManagement.Models;

namespace CafeManagement.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetByCategoryId(Guid IdCategory);
    }
}
