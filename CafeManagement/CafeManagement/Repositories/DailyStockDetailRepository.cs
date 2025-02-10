using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;

namespace CafeManagement.Repositories
{
    public class DailyStockDetailRepository:BaseRepository<DailyStockDetail>,IDailyStockDetailRepository
    {
        public DailyStockDetailRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
