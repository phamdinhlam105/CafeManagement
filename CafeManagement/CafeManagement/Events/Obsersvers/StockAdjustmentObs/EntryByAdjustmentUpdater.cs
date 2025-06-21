using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;

namespace CafeManagement.Events.Obsersvers.StockAdjustmentObs
{
    public class EntryByAdjustmentUpdater : IAppObserver<StockAdjustment>
    {
        private readonly IStockFIFOService _stockFIFOService;
        public EntryByAdjustmentUpdater(IStockFIFOService stockFIFOService)
        {
            _stockFIFOService = stockFIFOService;
        }

        public async Task Update(StockAdjustment data)
        {
            foreach (var detail in data.AdjustmentDetails)
            {
                await _stockFIFOService.UpdateEntry(detail.IngredientId, detail.QuantityAdjusted);
            }
        }
    }
}
