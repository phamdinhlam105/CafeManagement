using CafeManagement.Helpers;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Events.Obsersvers.FinishOrderObs
{
    public class OrderReportUpdater : IAppObserver<Order>
    {
        private readonly IOrderReportUpdateService _orderReportUpdateService;
        public OrderReportUpdater(IOrderReportUpdateService orderReportUpdateService)
        {
            _orderReportUpdateService = orderReportUpdateService;
        }
        public async Task Update(Order data)
        {
            await _orderReportUpdateService.UpdateOrderReportByOrder(data.Id);
        }
    }
}
