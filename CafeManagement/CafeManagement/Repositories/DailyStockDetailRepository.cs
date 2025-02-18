using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models.Stock;

namespace CafeManagement.Repositories
{
    public class DailyStockDetailRepository:BaseRepository<DailyStockDetail>,IDailyStockDetailRepository
    {
        public DailyStockDetailRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
