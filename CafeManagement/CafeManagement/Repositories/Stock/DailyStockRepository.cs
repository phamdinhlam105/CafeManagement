using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.Stock
{
    public class DailyStockRepository : BaseRepository<DailyStock>, IDailyStockRepository
    {
        public DailyStockRepository(CafeManagementDbContext _context) : base(_context) { }
        public override async Task<IEnumerable<DailyStock>> GetAll()
        {
            return await _context.DailyStocks
                .Include(ds => ds.DailyStockDetails)
                .ThenInclude(dt => dt.Ingredient).ToListAsync();
        }
    }
}
