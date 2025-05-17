using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models;
using CafeManagement.Models.Order;
using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Services.Report
{
    public class ReportUpdateService : IReportUpdateService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportUpdateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateOrderReport(Order order)
        {
            var todayReport = await _unitOfWork.DailyReport.GetByDate(Ultilities.GetToday());
            if (todayReport == null)
                todayReport = new DailyReport();
            foreach (OrderDetail detail in order.Details)
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

        public Task UpdateStockReport(StockEntry stockEntry)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProductReport(DailyReport todayReport)
        {
            todayReport.OrderReport.TopSelling = todayReport.ProductReports.OrderByDescending(pr => pr.QuantitySold).FirstOrDefault().Product;
            todayReport.OrderReport.LeastSelling = todayReport.ProductReports.OrderBy(pr => pr.QuantitySold).FirstOrDefault().Product;
            todayReport.IsOrderReportUpToDate = true;
            await _unitOfWork.DailyReport.Update(todayReport);
        }
        /*
        public async Task<List<int>> GetPeakHours(DateOnly date)
        {
            return (await _unitOfWork.Order.GetAll())
                .Where(o => DateOnly.FromDateTime(o.createdAt) == date)
                .GroupBy(o => o.createdAt.Hour)
                .Select(g => new { Hour = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(3)
                .Select(g => g.Hour)
                .ToList();
        }
        public async Task<IEnumerable<BestDays>> GetBestDaysInWeek(DateTime startDate, DateTime endDate)
        {
            var totalDays = (endDate.Date - startDate.Date).TotalDays + 1;
            var totalWeeks = (int)Math.Ceiling(totalDays / 7.0);

            var bestDays = (await _unitOfWork.Order.GetAll())
                .Where(o => o.createdAt >= startDate && o.createdAt <= endDate && o.OrderStatus == Enums.OrderStatus.Completed)
                .GroupBy(o => o.createdAt.DayOfWeek)
                .Select(g => new
                {
                    WeekDay = g.Key,
                    TotalRevenue = g.Sum(o => o.Price)
                })
                .Select(g => new
                {
                    WeekDay = g.WeekDay,
                    AverageRevenue = totalWeeks > 0 ? g.TotalRevenue / totalWeeks : 0
                })
                .OrderByDescending(g => g.AverageRevenue)
                .Take(2)
                .ToList();

            return bestDays.Select(day => new BestDays
            {
                WeekDay = day.WeekDay.ToString(),
                AvgRevenue = day.AverageRevenue
            });
        }
        */
    }
}
