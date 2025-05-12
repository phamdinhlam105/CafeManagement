using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.Stock
{
    public class DailyStockDetailRepository : BaseRepository<DailyStockDetail>, IDailyStockDetailRepository
    {
        public DailyStockDetailRepository(CafeManagementDbContext _context) : base(_context) { }
        
        public override async Task<IEnumerable<DailyStockDetail>> GetAll()
        {
            return await _context.DailyStockDetails.Include(dsd => dsd.Ingredient).ToListAsync();
        }
        public override async Task<DailyStockDetail> GetById(Guid id)
        {
            return await _context.DailyStockDetails
                .Include(dsd => dsd.Ingredient)
                .FirstOrDefaultAsync(dsd=>dsd.Id==id);
        }

    }
}
