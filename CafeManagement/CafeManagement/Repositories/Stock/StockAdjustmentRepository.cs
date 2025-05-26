using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;

namespace CafeManagement.Repositories.Stock
{
    public class StockAdjustmentRepository:BaseRepository<StockAdjustment>,IStockAdjustmentRepository
    {
        public StockAdjustmentRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
