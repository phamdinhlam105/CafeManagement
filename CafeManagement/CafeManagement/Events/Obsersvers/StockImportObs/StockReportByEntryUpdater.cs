using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;

namespace CafeManagement.Events.Obsersvers.StockImportObs
{
    public class StockReportByEntryUpdater : IAppObserver<StockEntry>
    {
        private readonly IStockReportUpdateService _stockReportUpdateService;
        public StockReportByEntryUpdater(IStockReportUpdateService stockReportUpdateService)
        {

            _stockReportUpdateService = stockReportUpdateService;
        }

        public async Task Update(StockEntry data)
        {
            await _stockReportUpdateService.UpdateStockReportByStockEntry(data);
        }
    }
}
