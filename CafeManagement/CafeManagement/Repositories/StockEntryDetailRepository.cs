using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models.Stock;

namespace CafeManagement.Repositories
{
    public class StockEntryDetailRepository:BaseRepository<StockEntryDetail>,IStockEntryDetailRepository
    {
        public StockEntryDetailRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
