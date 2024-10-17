using CafeManagement.Data;
using CafeManagement.Models;
using CafeManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
