using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.Stock;

namespace CafeManagement.Observers
{
    public class StockReportUpdater : IAppObserver<StockEntry>
    {
        private readonly IReportUpdateService _reportUpdateService;
        public StockReportUpdater(IReportUpdateService reportUpdateService)
        {
          
            _reportUpdateService = reportUpdateService;
        }

        public async Task Update(StockEntry data)
        {
            await _reportUpdateService.UpdateStockReport(data);
        }
    }
}
