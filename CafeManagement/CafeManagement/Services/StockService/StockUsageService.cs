
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.StockService
{
    public class StockUsageService : IStockUsageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockUsageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddStockUsageLogByOrder(Order order)
        {
            foreach (OrderDetail detail in order.Details)
            {
                var usageLog = new StockUsageLog
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = detail.ProductId,
                    SellingPrice = detail.Product.Price,
                    StockUsageDetails = new List<StockUsageDetail>(),
                    TotalCost = 0
                };

                var recipe = await _unitOfWork.Recipe.GetByProductId(detail.ProductId);
                if (recipe == null) continue;

                foreach (var recipeDetail in recipe.Details)
                {
                    var ingredient = recipeDetail.Ingredient;
                    var totalNeeded = recipeDetail.Amount * detail.Quantity;
                    var listDetail = await AddUsageDetailByIngredient(ingredient, totalNeeded);
                    usageLog.StockUsageDetails.AddRange(listDetail);
                    usageLog.TotalCost = listDetail.Sum(d => d.TotalValue);
                }
                await _unitOfWork.StockUsageLog.Add(usageLog);
            }
        }

        public async Task<List<StockUsageDetail>> AddUsageDetailByIngredient(Ingredient ingredient, float totalNeeded)
        {
            var usageDetailList = new List<StockUsageDetail>();
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
                usageDetailList.Add(usage);
            }
            return usageDetailList;
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
