using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.StockService
{
    public interface IStockEntryService
    {
        Task AddNewEntry(StockEntry entry);
        Task<List<StockEntry>> GetAll();
        Task<List<StockEntry>> GetByDate(DateOnly date);
    }
}
