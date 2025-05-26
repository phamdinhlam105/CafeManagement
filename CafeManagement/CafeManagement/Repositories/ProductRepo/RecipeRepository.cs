using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.ProductRepo;
using CafeManagement.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.ProductRepo
{
    public class RecipeRepository:BaseRepository<Recipe>,IRecipeRepository
    {
        public RecipeRepository(CafeManagementDbContext _context):base(_context) { }
        public async Task<Recipe> GetByProductId(Guid productId)
        {
            return await _context.Recipes
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId)
                .FirstOrDefaultAsync();
        }
    }
}
