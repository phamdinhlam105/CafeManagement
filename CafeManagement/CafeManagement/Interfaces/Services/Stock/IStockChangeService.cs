using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.Stock
{
    public interface IStockChangeService
    {
        Task StockImport(StockEntry entry);
        Task StockUpdate(StockAdjustment stockAdjustment);
        Task ExportForUse(Guid orderId);
    }
}
