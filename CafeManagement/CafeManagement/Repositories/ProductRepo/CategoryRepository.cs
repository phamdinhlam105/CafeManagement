﻿using CafeManagement.Data;
using Microsoft.EntityFrameworkCore;
using CafeManagement.Models.ProductModel;
using CafeManagement.Interfaces.Repositories.ProductRepo;

namespace CafeManagement.Repositories.ProductRepo
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CafeManagementDbContext _context) : base(_context) { }
        public override async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }
        public override async Task<Category> GetById(Guid id)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
