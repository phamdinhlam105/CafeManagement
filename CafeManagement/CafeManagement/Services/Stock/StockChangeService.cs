using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

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
                        StockImport = entryDetail.Quantity,
                        StockRemaining = entryDetail.Quantity,
                        IngredientId = entryDetail.IngredientId,
                    };
                    await _unitOfWork.DailyStock.Add(newStockDetail);
                }
                else
                {
                    stockDetail.StockImport += entryDetail.Quantity;
                    stockDetail.StockRemaining += entryDetail.Quantity;
                    await _unitOfWork.DailyStock.Update(stockDetail);
                }
            }
            await _unitOfWork.StockEntry.Add(entry);
        }

        public async Task StockUpdate(DailyStock dailyStock)
        {
            
        }

        public async Task ExportForUse(Guid orderId)
        {

        }

    }
}
