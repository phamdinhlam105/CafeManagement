
using CafeManagement.Data;
using CafeManagement.Models;
using CafeManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(CafeManagementDbContext _context):base(_context)
        {

        }
        public IEnumerable<Product> GetByCategoryId(Guid categoryId)
        {
            return _context.Products
                  .Where(p => p.Category.Id == categoryId) 
                   .ToList();
        }
    }
}
