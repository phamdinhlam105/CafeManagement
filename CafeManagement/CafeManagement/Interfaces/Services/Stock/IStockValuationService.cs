namespace CafeManagement.Interfaces.Services.Stock
{
    public interface IStockValuationService
    {
        Task UpdateStock(Guid ingredientId, float amount);
        Task<decimal> GetIngredientValueFIFO(Guid ingredientId, float amount);
    }
}
