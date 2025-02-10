using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;

namespace CafeManagement.Repositories
{
    public class StockEntryDetailRepository:BaseRepository<StockEntryDetail>,IStockEntryDetailRepository
    {
        public StockEntryDetailRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
