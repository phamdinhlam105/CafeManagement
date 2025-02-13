using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services
{
    public interface IStockEntryService
    {
        void StockImport(StockEntry entry);
        IEnumerable<StockEntry> GetAll();
        IEnumerable<StockEntry> GetByDate(DateOnly date);
    }
}
