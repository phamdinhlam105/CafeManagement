using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.ProductRepo;
using CafeManagement.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.ProductRepo
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(CafeManagementDbContext _context) : base(_context)
        {

        }
        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
               .Include(p => p.Category)
               .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryId(Guid categoryId)
        {
            return await _context.Products
                .Include(p => p.Category)
                  .Where(p => p.CategoryId == categoryId)
                  .ToListAsync();
        }

    }
}
