using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models;
using CafeManagement.Models.OrderModel;
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
          
        }

        public async Task UpdateStockReport(StockEntry stockEntry)
        {
         
        }

        public async Task UpdateProductReport(DailyReport todayReport)
        {
        
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
