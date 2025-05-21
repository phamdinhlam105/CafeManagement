using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.Stock;

namespace CafeManagement.Observers
{
    public class StockUpdater:IAppObserver<StockEntry>
    {
        private readonly IStockChangeService _stockChangeService;
        public StockUpdater(IStockChangeService stockChangeService)
        {
            _stockChangeService = stockChangeService;
        }

        public async Task Update(StockEntry data)
        {
            await _stockChangeService.StockImport(data);
        }
    }
}
