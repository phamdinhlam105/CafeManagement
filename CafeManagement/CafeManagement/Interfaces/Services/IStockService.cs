using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services
{
    public interface IStockService
    {
        DailyStock StockRemain();
        void StockImport(StockEntry entry);
        void StockUpdate(Guid stockId, Ingredient ingredient, float amountRemain);

    }
}
