using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Report
{
    public class OrderReportUpdateService : IOrderReportUpdateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportCreationService _reportCreationService;
        public OrderReportUpdateService(IUnitOfWork unitOfWork, IReportCreationService reportCreationService)
        {
            _unitOfWork = unitOfWork;
            _reportCreationService = reportCreationService;
        }

        public async Task UpdateOrderReportByOrder(Guid orderId)
        {
            var order = await _unitOfWork.Order.GetById(orderId);
            if (order == null)
                throw new Exception("Order Id not exist");
            DailyReport todayReport = (await _reportCreationService.GetTodayReport());
            foreach (OrderDetail detail in order.Details)
            {
                var productReport = todayReport.ProductReports.FirstOrDefault(pr => pr.ProductId == detail.ProductId);
                if (productReport == null)
                {
                    productReport = new ProductReport
                    {
                        Id = new Guid(),
                        DailyReportId = todayReport.Id,
                        QuantitySold = detail.Quantity,
                        Product = detail.Product
                    };
                    todayReport.ProductReports.Add(productReport);
                }
                else
                    productReport.QuantitySold += detail.Quantity;
            }
            todayReport.OrderReport.TotalRevenue += order.Price;
            todayReport.OrderReport.NumberOfFinishedOrders++;
            todayReport.OrderReport.TotalProductsSold += order.Quantity;
            todayReport.IsOrderReportUpToDate = false;
            await _unitOfWork.DailyReport.Update(todayReport);
        }
    }
}
