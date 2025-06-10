using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.StockService
{
    public class StockFIFOService : IStockFIFOService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockFIFOService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateEntry(Guid ingredientId, float amount)
        {
            var availableStock = await _unitOfWork.StockEntryDetail.GetAvailableIngredient(ingredientId);
            foreach (var stockDetail in availableStock)
            {
                if (amount <= 0)
                    break;
                if (stockDetail.RemainQuantity <= 0) continue;
                if(stockDetail.RemainQuantity >= amount)
                {
                    stockDetail.RemainQuantity -= amount;
                    amount = 0;
                    await _unitOfWork.StockEntryDetail.Update(stockDetail);
                    break;
                }
                else
                {
                    amount -= stockDetail.RemainQuantity;
                    stockDetail.RemainQuantity = 0;
                    await _unitOfWork.StockEntryDetail.Update(stockDetail);
                }
            }
        }
        public async Task<decimal> GetIngredientValueFIFO(Guid ingredientId, float amount)
        {
            var availableStock = await _unitOfWork.StockEntryDetail.GetAvailableIngredient(ingredientId);
            decimal totalValue = 0;
            foreach (var stockDetail in availableStock)
            {
                if (amount <= 0)
                    break;
                if (stockDetail.RemainQuantity <= 0) continue;
                if (stockDetail.RemainQuantity >= amount)
                {
                    totalValue += stockDetail.Price * (decimal)amount;
                    break;
                }
                else
                {
                    amount -= stockDetail.RemainQuantity;
                    totalValue += (decimal)stockDetail.RemainQuantity * stockDetail.Price;
                }
            }
            return totalValue;
        }
    }
}
