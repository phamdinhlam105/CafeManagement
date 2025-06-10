using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.Stock
{
    public class StockUsageLogRepository:BaseRepository<StockUsageLog>,IStockUsageLogRepository
    {
        public StockUsageLogRepository(CafeManagementDbContext _context) : base(_context) { }
        public async Task<List<StockUsageLog>> GetByOrderId(Guid orderId)
        {
            return await _context.StockUsageLogs
                .Include(sul => sul.StockUsageDetails)
                .Where(sul => sul.OrderId == orderId).ToListAsync();
        }
        public async Task<List<StockUsageLog>> GetByDate(DateOnly date)
        {
            return await _context.StockUsageLogs
                .Include(sul => sul.StockUsageDetails)
                .Where(sul => DateOnly.FromDateTime(sul.UsedAt) == date).ToListAsync();
        }
    }
}
