using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;

namespace CafeManagement.Repositories.Stock
{
    public class StockEntryDetailRepository : BaseRepository<StockEntryDetail>, IStockEntryDetailRepository
    {
        public StockEntryDetailRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
