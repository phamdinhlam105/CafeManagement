using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;

namespace CafeManagement.Events.Obsersvers.StockUsageLogObs
{
    public class EntryByUsageUpdater : IAppObserver<StockUsageLog>
    {
        private readonly IStockFIFOService _stockFIFOService;
        public EntryByUsageUpdater(IStockFIFOService stockFIFOService)
        {
            _stockFIFOService = stockFIFOService;
        }

        public async Task Update(StockUsageLog data)
        {
            foreach (var detail in data.StockUsageDetails)
            {
                await _stockFIFOService.UpdateEntry(detail.StockEntryDetail.IngredientId, detail.QuantityUsed);
            }
        }
    }
}
