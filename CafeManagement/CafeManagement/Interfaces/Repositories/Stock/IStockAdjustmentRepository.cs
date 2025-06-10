using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Repositories.Stock
{
    public interface IStockAdjustmentRepository:IRepository<StockAdjustment>
    {
        Task<List<StockAdjustment>> GetByDate(DateOnly date);
        Task<List<StockAdjustment>> GetByDateRange(DateOnly start, DateOnly end);
    }
}
