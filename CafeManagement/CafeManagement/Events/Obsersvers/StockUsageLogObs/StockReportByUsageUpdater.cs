using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Stock;

namespace CafeManagement.Events.Obsersvers.StockUsageLogObs
{
    public class StockReportByUsageUpdater : IAppObserver<StockUsageLog>
    {
        private readonly IStockReportUpdateService _stockReportUpdateService;
        public StockReportByUsageUpdater(IStockReportUpdateService stockReportUpdateService)
        {
            _stockReportUpdateService = stockReportUpdateService;
        }

        public async Task Update(StockUsageLog data)
        {
            await _stockReportUpdateService.UpdateStockReportByUsage(data.Id);
        }
    }
}
