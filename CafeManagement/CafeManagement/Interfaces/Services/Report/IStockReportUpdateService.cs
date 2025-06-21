using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IStockReportUpdateService
    {
        Task UpdateStockReportByStockEntry(StockEntry entry);
        Task UpdateStockReportByAdjustment(StockAdjustment adjustment);
        Task UpdateStockReportByUsage(Guid orderId);
    }
}
