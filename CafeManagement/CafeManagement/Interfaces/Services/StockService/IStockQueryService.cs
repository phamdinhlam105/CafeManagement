using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.StockService
{
    public interface IStockQueryService
    {
        Task<StockReport> GetStockAtAtime(DateOnly date);
        Task<StockReportDetail> GetStockByIngredient(Guid ingredientId);
    }
}
