using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.Stock
{
    public class StockEntryRepository : BaseRepository<StockEntry>, IStockEntryRepository
    {
        public StockEntryRepository(CafeManagementDbContext _context) : base(_context) { }
        public override IEnumerable<StockEntry> GetAll()
        {
            return _context.StockEntries
                .Include(se => se.StockEntryDetails)
                .ThenInclude(dt => dt.Ingredient);
        }
    }
}
