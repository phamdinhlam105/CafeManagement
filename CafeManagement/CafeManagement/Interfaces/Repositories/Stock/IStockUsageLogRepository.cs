using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Repositories.Stock
{
    public interface IStockUsageLogRepository:IRepository<StockUsageLog>
    {
        Task<List<StockUsageLog>> GetByOrderId(Guid orderId);
        Task<List<StockUsageLog>> GetByDate(DateOnly date);
    }
}
