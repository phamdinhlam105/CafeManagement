using CafeManagement.Helpers;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.Order;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.ObserverPattern
{
    public class ReportUpdater : IObserver
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportUpdater(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Update(object data)
        {
            var todayReport = await _unitOfWork.DailyReport.GetByDate(Ultilities.GetToday());
            if (todayReport == null)
                todayReport = new DailyReport();
            if (data is Order order)
            {
                foreach(OrderDetail detail in order.Details)
                {
                    var productReport = todayReport.ProductReports.FirstOrDefault(pr => pr.ProductId == detail.ProductId);
                    if (productReport == null)
                    {
                        productReport = new ProductReport
                        {
                            Id = new Guid(),
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
}
