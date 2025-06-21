using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.Stock
{
    public class StockUsageDetailRepository: BaseRepository<StockUsageDetail>, IStockUsageDetailRepository
    {
        public StockUsageDetailRepository(CafeManagementDbContext _context) : base(_context) { }
        public async Task<List<StockUsageDetail>> GetDetailListByOrderId(Guid orderId)
        {
            return await _context.StockUsageDetails
                .Include(sud => sud.StockUsageLog)
                .Include(sud=>sud.StockEntryDetail)
                .Where(sud => sud.StockUsageLog.OrderId == orderId).ToListAsync();
        }
        public async Task<List<StockUsageDetail>> GetDetailListByUsageId(Guid usageId)
        {
            return await _context.StockUsageDetails
                .Include(sud => sud.StockUsageLog)
                .Include(sud => sud.StockEntryDetail)
                .Where(sud => sud.StockUsageLogId == usageId).ToListAsync();
        }
    }
}
