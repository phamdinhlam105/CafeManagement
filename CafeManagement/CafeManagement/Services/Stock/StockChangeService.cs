using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.ProductModel;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;
using iText.Layout.Borders;
using static iText.IO.Util.IntHashtable;

namespace CafeManagement.Services.Stock
{
    public class StockChangeService : IStockChangeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockService _stockService;
        public StockChangeService(IUnitOfWork unitOfWork, IStockService stockService)
        {
            _unitOfWork = unitOfWork;
            _stockService = stockService;
        }

        public async Task StockImport(StockEntry entry)
        {
            var dailyStock = await _stockService.StockRemain();

            foreach (var entryDetail in entry.StockEntryDetails)
            {
                var stockDetail = dailyStock
                    .FirstOrDefault(d => d.IngredientId == entryDetail.IngredientId);
                if (stockDetail == null)
                {
                    var newStockDetail = new DailyStock
                    {
                        Id = Guid.NewGuid(),
                        StockAtStartOfDay = 0,
                        StockImport = entryDetail.ImportQuantity,
                        StockRemaining = entryDetail.ImportQuantity,
                        IngredientId = entryDetail.IngredientId,
                    };
                    await _unitOfWork.DailyStock.Add(newStockDetail);
                }
                else
                {
                    stockDetail.StockImport += entryDetail.ImportQuantity;
                    stockDetail.StockRemaining += entryDetail.ImportQuantity;
                    await _unitOfWork.DailyStock.Update(stockDetail);
                }
            }
            await _unitOfWork.StockEntry.Add(entry);
        }

        public async Task StockUpdate(StockAdjustment stockAdjustment)
        {
            await _unitOfWork.StockAdjustment.Add(stockAdjustment);
        }
        
        public async Task ExportForUse(Guid orderId)
        {
            var order = await _unitOfWork.Order.GetById(orderId);

            foreach (OrderDetail detail in order.Details)
            {
                var usageLog = new StockUsageLog
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
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
                    var entryDetails = await _unitOfWork.StockEntryDetail
                        .GetAvailableIngredient(ingredient.Id);
                    if (entryDetails.Sum(e => e.RemainQuantity) < totalNeeded)
                        throw new Exception($"Not enough stock for ingredient {ingredient.Name}");
                    foreach (var entry in entryDetails)
                    {
                        if (totalNeeded <= 0) break;
                        if (entry.RemainQuantity <= 0) continue;

                        var usedQuantity = Math.Min(entry.RemainQuantity, totalNeeded);
                        totalNeeded -= usedQuantity;

                        var usage = new StockUsageDetail
                        {
                            Id = Guid.NewGuid(),
                            StockUsageLog = usageLog,
                            StockEntryDetailId = entry.Id,
                            QuantityUsed = usedQuantity,
                            TotalValue = (decimal)usedQuantity * entry.Price
                        };

                        usageLog.TotalCost += usage.TotalValue;
                        usageLog.StockUsageDetails.Add(usage);
                    }
                }
                await _unitOfWork.StockUsageLog.Add(usageLog);
            }
        }
    }
}
