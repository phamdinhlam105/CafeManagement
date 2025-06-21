using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Events.Obsersvers.FinishOrderObs
{
    public class StockReportByOrderUpdater : IAppObserver<Order>
    {
        private readonly IStockReportUpdateService _stockreportUpdaterService;
        public StockReportByOrderUpdater(IStockReportUpdateService stockreportUpdaterService)
        {
            _stockreportUpdaterService = stockreportUpdaterService;
        }
        public async Task Update(Order data)
        {
            await _stockreportUpdaterService.UpdateStockReportByOrder(data.Id);
        }
    }
}
