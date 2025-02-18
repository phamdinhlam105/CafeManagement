using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
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

        public List<int> GetPeakHours(DateTime date)
        {
            return _unitOfWork.Order.GetAll()
                .Where(o => o.createdAt.Date == date)
                .GroupBy(o => o.createdAt.Hour)
                .Select(g => new { Hour = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(3)
                .Select(g => g.Hour)
                .ToList();
        }

        public DateOnly GetBestDayInWeek(int month, int year)
        {
            return _unitOfWork.Order.GetAll()
                .Where(o => o.createdAt.Month == month && o.createdAt.Year == year)
                .GroupBy(o => o.createdAt.Day)
                .Select(g => new { Day = g.Key, TotalRevenue = g.Sum(o => o.Price) })
                .OrderByDescending(g => g.TotalRevenue)
                .Select(g => DateOnly.FromDateTime(new DateTime(year, month, g.Day)))
                .FirstOrDefault();
        }
    }
}
