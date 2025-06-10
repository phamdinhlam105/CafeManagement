using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.Stock;

namespace CafeManagement.Events.Obsersvers.StockReportObserver
{
    public class StockReportByAdjustmentUpdater : IAppObserver<StockAdjustment>
    {
        private readonly IStockReportUpdateService _stockreportUpdaterService;
        public StockReportByAdjustmentUpdater(IStockReportUpdateService stockreportUpdaterService)
        {
            _stockreportUpdaterService = stockreportUpdaterService;
        }
        public async Task Update(StockAdjustment data)
        {
            await _stockreportUpdaterService.UpdateStockReportByAdjustment(data);
        }
    }
}
