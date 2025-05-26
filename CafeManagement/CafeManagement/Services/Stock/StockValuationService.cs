using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;
using static iText.IO.Util.IntHashtable;

namespace CafeManagement.Services.Stock
{
    public class StockValuationService : IStockValuationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockValuationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateStock(Guid ingredientId, float amount)
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
