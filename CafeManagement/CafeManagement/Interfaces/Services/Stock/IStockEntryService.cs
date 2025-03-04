using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.Stock
{
    public interface IStockEntryService
    {
        Task StockImport(StockEntry entry);
        Task<IEnumerable<StockEntry>> GetAll();
        Task<IEnumerable<StockEntry>> GetByDate(DateOnly date);
    }
}
