using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.Stock
{
    public class StockEntryDetailRepository : BaseRepository<StockEntryDetail>, IStockEntryDetailRepository
    {
        public StockEntryDetailRepository(CafeManagementDbContext _context) : base(_context) { }
        public override async Task<IEnumerable<StockEntryDetail>> GetAll()
        {
            return await _context.StockEntryDetails.Include(sed => sed.Ingredient).ToListAsync();
        }
        public override async Task<StockEntryDetail> GetById(Guid id)
        {
            return await _context.StockEntryDetails
                .Include(sed => sed.Ingredient)
                .FirstOrDefaultAsync(sed => sed.Id == id);
        }
    }
}
