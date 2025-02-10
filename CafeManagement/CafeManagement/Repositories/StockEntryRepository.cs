using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;

namespace CafeManagement.Repositories
{
    public class StockEntryRepository:BaseRepository<StockEntry>,IStockEntryRepository
    {
        public StockEntryRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
