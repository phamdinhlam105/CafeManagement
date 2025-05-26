using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IStockImportUpdateService
    {
        Task UpdateStockReportByStockEntry(StockEntry entry);
    }
}
