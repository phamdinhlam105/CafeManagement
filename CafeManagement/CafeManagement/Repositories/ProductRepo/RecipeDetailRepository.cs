using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.ProductRepo;
using CafeManagement.Models.ProductModel;

namespace CafeManagement.Repositories.ProductRepo
{
    public class RecipeDetailRepository:BaseRepository<RecipeDetail>,IRecipeDetailRepository
    {
        public RecipeDetailRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
