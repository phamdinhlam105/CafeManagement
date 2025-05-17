using CafeManagement.Helpers;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Order;

namespace CafeManagement.Observers
{
    public class OrderReportUpdater : IAppObserver<Order>
    {
        private readonly IReportUpdateService _reportUpdateService;
        public OrderReportUpdater(IReportUpdateService reportUpdateService)
        {
            _reportUpdateService = reportUpdateService;
        }
        public async Task Update(Order data)
        {
            await _reportUpdateService.UpdateOrderReport(data);
        }
    }
}
