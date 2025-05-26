using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;
using iText.Layout.Borders;

namespace CafeManagement.Services.Report
{
    public class OrderCompleteUpdateService : IOrderCompleteUpdateService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderCompleteUpdateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        private async Task<DailyReport> GetToDayReport()
        {
            var todayReport = await _unitOfWork.DailyReport.GetByDate(Ultilities.GetToday());
            if (todayReport == null)
                todayReport = new DailyReport();
            return todayReport;
        }
        public async Task UpdateOrderReportByOrder(Guid orderId)
        {
            var order = await _unitOfWork.Order.GetById(orderId);
            if (order == null)
                throw new Exception("Order Id not exist");
            DailyReport todayReport = await GetToDayReport();
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

        public async Task UpdateStockReportByOrder(Guid orderId)
        {
            var listUsage = await _unitOfWork.StockUsageLog.GetByOrderId(orderId);

        }
    }
}
