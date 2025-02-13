using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class StockEntryRepository:BaseRepository<StockEntry>,IStockEntryRepository
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
