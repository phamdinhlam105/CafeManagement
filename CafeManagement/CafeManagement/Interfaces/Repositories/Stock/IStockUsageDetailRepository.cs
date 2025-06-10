using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Repositories.Stock
{
    public interface IStockUsageDetailRepository:IRepository<StockUsageDetail>
    {
        Task<List<StockUsageDetail>> GetDetailListByOrderId(Guid orderId);
    }
}
