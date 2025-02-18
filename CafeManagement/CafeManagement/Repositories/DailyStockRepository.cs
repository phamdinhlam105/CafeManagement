using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models.Stock;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class DailyStockRepository:BaseRepository<DailyStock>,IDailyStockRepository
    {
        public DailyStockRepository(CafeManagementDbContext _context) : base(_context) { }
        public override IEnumerable<DailyStock> GetAll()
        {
            return _context.DailyStocks
                .Include(ds => ds.DailyStockDetails)
                .ThenInclude(dt=>dt.Ingredient);
        }
    }
}
