using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.Stock
{
    public interface IStockService
    {
        Task<IEnumerable<DailyStock>> StockRemain();
        Task StockUpdate(Guid ingredientId, float amountRemain);
        Task<IEnumerable<DailyStock>> GetAllDailyStocks();
        Task<IEnumerable<DailyStock>> GetByDate(DateOnly date);
    }
}
