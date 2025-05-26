using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.Stock
{
    public interface IStockCalculationService
    {
        Task<List<DailyStock>> CurrentStock();
        Task<List<DailyStock>> GetStockAtAtime(DateOnly date);
        Task<DailyStock> GetStockByIngredient(Guid ingredientId);
    }
}
