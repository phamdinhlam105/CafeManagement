using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Repositories.Stock
{
    public interface IStockEntryDetailRepository : IRepository<StockEntryDetail>
    {
        Task<List<StockEntryDetail>> GetAvailableIngredient(Guid ingredientId);
    }
}
