﻿using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Services.Report
{
    public class ReportQueryService : IReportQueryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<decimal> GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            return (await _unitOfWork.Order.GetAll())
                .Where(o => o.createdAt >= startDate && o.createdAt <= endDate && o.OrderStatus == Enums.OrderStatus.Completed)
                .Sum(o => o.Price);
        }

        public async Task<decimal> GetTotalExpenditure(DateTime startDate, DateTime endDate)
        {
            return (await _unitOfWork.StockEntry.GetAll())
                .Where(s => s.EntryDate >= startDate && s.EntryDate <= endDate)
                .Sum(s => s.TotalValue);
        }

        public async Task<int> GetNumberOfFinishedOrders(DateTime startDate, DateTime endDate)
        {
            return (await _unitOfWork.Order.GetAll())
                .Count(o => o.createdAt >= startDate && o.createdAt <= endDate && o.OrderStatus == Enums.OrderStatus.Completed);
        }

        public async Task<int> GetNumberOfCancelledOrders(DateTime startDate, DateTime endDate)
        {
            return (await _unitOfWork.Order.GetAll())
                .Count(o => o.createdAt >= startDate && o.createdAt <= endDate && o.OrderStatus == Enums.OrderStatus.Cancelled);
        }

        public async Task<Product> GetTopSellingProduct(DateTime startDate, DateTime endDate)
        {
            return (await _unitOfWork.OrderDetail.GetAll())
                .Where(od => od.Order.createdAt >= startDate && od.Order.createdAt <= endDate)
                .GroupBy(od => od.ProductId)
                .Select(g => new { g.First().Product, TotalSold = g.Sum(od => od.Quantity) })
                .OrderByDescending(g => g.TotalSold)
                .Select(g => g.Product)
                .FirstOrDefault();
        }

        public async Task<Product> GetLeastSellingProduct(DateTime startDate, DateTime endDate)
        {
            return (await _unitOfWork.OrderDetail.GetAll())
                .Where(od => od.Order.createdAt >= startDate && od.Order.createdAt <= endDate)
                .GroupBy(od => od.ProductId)
                .Select(g => new { g.First().Product, TotalSold = g.Sum(od => od.Quantity) })
                .OrderBy(g => g.TotalSold)
                .Select(g => g.Product)
                .FirstOrDefault();
        }

        public async Task<int> GetTotalProductsSold(DateTime startTime, DateTime endTime)
        {
            return (await _unitOfWork.Order.GetAll())
                .Where(o => o.createdAt >= startTime && o.createdAt <= endTime && o.OrderStatus == Enums.OrderStatus.Completed)
                .Sum(o => o.Details.Sum(od => od.Quantity));
        }

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
    }
}
