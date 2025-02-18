using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models.Stock;

namespace CafeManagement.Repositories
{
    public class IngredientRepository:BaseRepository<Ingredient>,IIngredientRepository
    {
        public IngredientRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
