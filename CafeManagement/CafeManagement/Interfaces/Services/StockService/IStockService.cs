using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.StockService
{
    public interface IStockService
    {
        Task StockImport(StockEntry entry);
        Task StockAdjustment(StockAdjustment adjustment);
        Task<List<DailyStock>> GetStockDetailByDate(DateOnly date);
        Task<DailyStock> GetStockByIngredientId(Guid ingredientId);
    }
}
