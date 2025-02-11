using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services
{
    public interface IStockService
    {
        DailyStock StockRemain();

        void StockUpdate(Guid stockId, Ingredient ingredient, float amountRemain);
        DailyStock NewDailyStock();
        IEnumerable<DailyStock> GetAllDailyStocks();
        IEnumerable<DailyStockDetail> GetDetailByDate(DateTime date);
    }
}
