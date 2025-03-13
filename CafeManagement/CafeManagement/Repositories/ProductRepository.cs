
using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(CafeManagementDbContext _context):base(_context)
        {

        }
        
        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                .Include(o => o.Category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryId(Guid categoryId)
        {
            return await _context.Products
                .Include(p=>p.Category)
                  .Where(p => p.Category.Id == categoryId) 
                   .ToListAsync();
        }
    }
}
