using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.Stock
{
    public interface IStockEntryService
    {
        void StockImport(StockEntry entry);
        IEnumerable<StockEntry> GetAll();
        IEnumerable<StockEntry> GetByDate(DateOnly date);
    }
}
