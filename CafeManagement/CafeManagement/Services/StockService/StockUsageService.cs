
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.StockService
{
    public class StockUsageService : IStockUsageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubject<StockUsageLog> _newUsageLogEvent;
        public StockUsageService(IUnitOfWork unitOfWork, 
            ISubject<StockUsageLog> newUsageLogEvent,
            IEventRegister<StockUsageLog> newUsageLogRegister)
        {
            _unitOfWork = unitOfWork;
            _newUsageLogEvent = newUsageLogEvent;
            newUsageLogRegister.Register(_newUsageLogEvent);
        }

        private async Task AddUsageDetailByIngredient(
           Ingredient ingredient,
           float totalNeeded,
           StockUsageLog usageLog)
        {
            var entryDetails = await _unitOfWork.StockEntryDetail
                .GetAvailableIngredient(ingredient.Id);

            foreach (var entry in entryDetails)
            {
                if (totalNeeded <= 0) break;
                if (entry.RemainQuantity <= 0) continue;

                var usedQuantity = Math.Min(entry.RemainQuantity, totalNeeded);
                totalNeeded -= usedQuantity;

                var usage = new StockUsageDetail
                {
                    Id = Guid.NewGuid(),
                    StockEntryDetailId = entry.Id,
                    QuantityUsed = usedQuantity,
                    TotalValue = (decimal)usedQuantity * entry.Price
                };

                usageLog.StockUsageDetails.Add(usage);
            }
        }

        public async Task AddStockUsageLogByOrder(Order order)
        {
            foreach (OrderDetail detail in order.Details)
            {
                var usageLog = new StockUsageLog
                {
                    Id = Guid.NewGuid(),
                    OrderDetailId=detail.Id,
                    SellingPrice = detail.Product.Price,
                    UsedAt=DateTime.UtcNow,
                    StockUsageDetails = new List<StockUsageDetail>(),
                    TotalCost = 0
                };

                var recipe = await _unitOfWork.Recipe.GetByProductId(detail.ProductId);
                if (recipe == null) continue;

                foreach (var recipeDetail in recipe.Details)
                {
                    var ingredient = recipeDetail.Ingredient;
                    var totalNeeded = recipeDetail.Amount * detail.Quantity;
                    await AddUsageDetailByIngredient(ingredient, totalNeeded,usageLog);
                    usageLog.TotalCost = usageLog.StockUsageDetails.Sum(d => d.TotalValue);
                }
                await _unitOfWork.StockUsageLog.Add(usageLog);
                await _newUsageLogEvent.Notify(usageLog);

            }
        }

        public async Task<List<StockUsageLog>> GetUsageLogByDate(DateOnly date)
        {
            return await _unitOfWork.StockUsageLog.GetByDate(date);
        }

        public async Task<List<StockUsageLog>> GetUsageLogByOrder(Guid orderId)
        {
            return await _unitOfWork.StockUsageLog.GetByOrderId(orderId);
        }
    }
}
