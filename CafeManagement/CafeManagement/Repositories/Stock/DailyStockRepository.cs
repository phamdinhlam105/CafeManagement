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
                .Include(dt => dt.Ingredient).ToListAsync();
        }

        public async Task<IEnumerable<DailyStock>> GetByDate(DateOnly date)
        {
            return await _context.DailyStocks
                .Include(dsd=>dsd.Ingredient)
                .Where(ds=>ds.CreateDate == date).ToListAsync();
        }

        public async Task<IEnumerable<DailyStock>> GetLastestStock()
        {
            var latestDate = await _context.DailyStocks
                .MaxAsync(ds => ds.CreateDate);
            return await _context.DailyStocks
                .Include(dsd => dsd.Ingredient)
                .Where(ds => ds.CreateDate == latestDate)
                .ToListAsync();
        }
    }
}
