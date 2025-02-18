using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;

namespace CafeManagement.Repositories.Stock
{
    public class DailyStockDetailRepository : BaseRepository<DailyStockDetail>, IDailyStockDetailRepository
    {
        public DailyStockDetailRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
