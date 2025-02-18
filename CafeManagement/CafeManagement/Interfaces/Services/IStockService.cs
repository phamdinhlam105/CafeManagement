using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services
{
    public interface IStockService
    {
        DailyStock StockRemain();

        void StockUpdate(Guid stockId, Ingredient ingredient, float amountRemain);
        DailyStock NewDailyStock();
        IEnumerable<DailyStock> GetAllDailyStocks();
        IEnumerable<DailyStockDetail> GetDetailByDate(DateOnly date);
    }
}
