using CafeManagement.Dtos.Respone.StockRes;
using CafeManagement.Dtos.Response.StockRes;

namespace CafeManagement.Interfaces.Facade.StockFacade
{
    public interface IStockQueryUseCase
    {
        Task<StockResponse> GetStockDetailByDate(DateOnly? date);
        Task<StockDetailResponse> GetStockByIngredientId(Guid ingredientId);
        Task<List<StockAdjustmentResponse>> GetAdjustmentsByDate(DateOnly? date);
        Task<List<StockEntryResponse>> GetEntriesByDate(DateOnly? date);
    }
}
