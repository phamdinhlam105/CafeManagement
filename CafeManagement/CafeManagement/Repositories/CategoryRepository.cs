using CafeManagement.Data;
using CafeManagement.Models;
using Microsoft.EntityFrameworkCore;
using CafeManagement.Interfaces.Repositories;

namespace CafeManagement.Repositories
{
    public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(CafeManagementDbContext _context) : base(_context) { }
        public override async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.Include(c=>c.Products).ToListAsync();
        }
    }
}
