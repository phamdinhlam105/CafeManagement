namespace CafeManagement.Interfaces.Services.StockService
{
    public interface IStockFIFOService
    {
        Task UpdateEntry(Guid ingredientId, float amount);
        Task<decimal> GetIngredientValueFIFO(Guid ingredientId, float amount);
    }
}
