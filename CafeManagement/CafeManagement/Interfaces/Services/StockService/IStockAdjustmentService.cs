using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.StockService
{
    public interface IStockAdjustmentService
    {
        Task NewAdjustment(StockAdjustment adjustment);
        Task<List<StockAdjustment>> GetAdjustmentsByDate(DateOnly date);
        Task<List<StockAdjustment>> GetAdjustmentsByRange(DateOnly start, DateOnly end);
    }
}
