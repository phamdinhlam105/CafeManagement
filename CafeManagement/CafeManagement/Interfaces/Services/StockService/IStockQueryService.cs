using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.StockService
{
    public interface IStockQueryService
    {
        Task<List<DailyStock>> CurrentStock();
        Task<List<DailyStock>> GetStockAtAtime(DateOnly date);
        Task<DailyStock> GetStockByIngredient(Guid ingredientId);
    }
}
