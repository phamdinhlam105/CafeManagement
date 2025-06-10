using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.StockService
{
    public interface IStockEntryService
    {
        Task AddNewEntry(StockEntry entry);
        Task<IEnumerable<StockEntry>> GetAll();
        Task<IEnumerable<StockEntry>> GetByDate(DateOnly date);
    }
}
