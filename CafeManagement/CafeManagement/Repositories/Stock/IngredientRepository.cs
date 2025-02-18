using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;

namespace CafeManagement.Repositories.Stock
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
