using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;

namespace CafeManagement.Repositories.Stock
{
    public class StockUsageDetailRepository: BaseRepository<StockUsageDetail>, IStockUsageDetailRepository
    {
        public StockUsageDetailRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
