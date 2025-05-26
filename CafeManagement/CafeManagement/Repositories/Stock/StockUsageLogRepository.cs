using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;

namespace CafeManagement.Repositories.Stock
{
    public class StockUsageLogRepository:BaseRepository<StockUsageLog>,IStockUsageLogRepository
    {
        public StockUsageLogRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
