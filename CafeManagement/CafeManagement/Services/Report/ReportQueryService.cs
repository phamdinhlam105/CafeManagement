using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Services.Report
{
    public class ReportQueryService : IReportQueryService
    {
        private IUnitOfWork _unitOfWork;
        public ReportQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public decimal GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Order.GetAll()
                .Where(o => o.createdAt >= startDate && o.createdAt <= endDate && o.OrderStatus == Enums.OrderStatus.Completed)
                .Sum(o => o.Price);
        }

        public decimal GetTotalExpenditure(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.StockEntry.GetAll()
                .Where(s => s.EntryDate >= startDate && s.EntryDate <= endDate)
                .Sum(s => s.TotalValue);
        }

        public int GetNumberOfFinishedOrders(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Order.GetAll()
                .Count(o => o.createdAt >= startDate && o.createdAt <= endDate && o.OrderStatus == Enums.OrderStatus.Completed);
        }

        public int GetNumberOfCancelledOrders(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Order.GetAll()
                .Count(o => o.createdAt >= startDate && o.createdAt <= endDate && o.OrderStatus == Enums.OrderStatus.Cancelled);
        }

        public Product GetTopSellingProduct(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.OrderDetail.GetAll()
                .Where(od => od.Order.createdAt >= startDate && od.Order.createdAt <= endDate)
                .GroupBy(od => od.ProductId)
                .Select(g => new { g.First().Product, TotalSold = g.Sum(od => od.Quantity) })
                .OrderByDescending(g => g.TotalSold)
                .Select(g => g.Product)
                .FirstOrDefault();
        }

        public Product GetLeastSellingProduct(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.OrderDetail.GetAll()
                .Where(od => od.Order.createdAt >= startDate && od.Order.createdAt <= endDate)
                .GroupBy(od => od.ProductId)
                .Select(g => new { g.First().Product, TotalSold = g.Sum(od => od.Quantity) })
                .OrderBy(g => g.TotalSold)
                .Select(g => g.Product)
                .FirstOrDefault();
        }

        public int GetTotalProductsSold(DateTime startTime, DateTime endTime)
        {
            return _unitOfWork.Order.GetAll()
                .Where(o => o.createdAt >= startTime && o.createdAt <= endTime && o.OrderStatus == Enums.OrderStatus.Completed)
                .Sum(o => o.Details.Sum(od => od.Quantity));
        }

        public List<int> GetPeakHours(DateOnly date)
        {
            return _unitOfWork.Order.GetAll()
                .Where(o => DateOnly.FromDateTime(o.createdAt) == date)
                .GroupBy(o => o.createdAt.Hour)
                .Select(g => new { Hour = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(3)
                .Select(g => g.Hour)
                .ToList();
        }

        public IEnumerable<BestDays> GetBestDaysInWeek(DateTime startDate, DateTime endDate, Guid reportId)
        {
            var totalDays = (endDate.Date - startDate.Date).TotalDays + 1;
            var totalWeeks = (int)Math.Ceiling(totalDays / 7.0);

            var bestDays = _unitOfWork.Order.GetAll()
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
                Id = Guid.NewGuid(),
                ReportId = reportId,
                WeekDay = day.WeekDay.ToString(),
                AvgRevenue = day.AverageRevenue
            });
        }
    }
}
