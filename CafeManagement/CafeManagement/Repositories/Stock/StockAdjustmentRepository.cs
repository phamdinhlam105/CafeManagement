using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CafeManagement.Repositories.Stock
{
    public class StockAdjustmentRepository : BaseRepository<StockAdjustment>, IStockAdjustmentRepository
    {
        public StockAdjustmentRepository(CafeManagementDbContext _context) : base(_context) { }

        public async Task<List<StockAdjustment>> GetByDate(DateOnly date)
        {
            return await _context.StockAdjustments
                .Include(sa => sa.AdjustmentDetails)
                .Where(sa => DateOnly.FromDateTime(sa.AdjustmentDate) == date)
                .ToListAsync();
        }

        public async Task<List<StockAdjustment>> GetByDateRange(DateOnly start, DateOnly end)
        {
            return await _context.StockAdjustments
                .Include(sa => sa.AdjustmentDetails)
                .Where(sa => DateOnly.FromDateTime(sa.AdjustmentDate) >= start && DateOnly.FromDateTime(sa.AdjustmentDate) <= end)
                .ToListAsync();
        }
    }
}
