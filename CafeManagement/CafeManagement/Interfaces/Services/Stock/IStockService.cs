using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.Stock
{
    public interface IStockService
    {
        Task<DailyStock> StockRemain();

        Task StockUpdate(Guid ingredientId, float amountRemain);
        Task<DailyStock> NewDailyStock();
        Task<IEnumerable<DailyStock>> GetAllDailyStocks();
        Task<IEnumerable<DailyStockDetail>> GetDetailByDate(DateOnly date);
    }
}
