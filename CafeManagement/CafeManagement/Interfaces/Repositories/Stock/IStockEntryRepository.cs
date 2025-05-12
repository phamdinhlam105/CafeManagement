using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Repositories.Stock
{
    public interface IStockEntryRepository : IRepository<StockEntry>
    {
        Task<IEnumerable<StockEntry>> GetByDate(DateOnly date);
    }
}
