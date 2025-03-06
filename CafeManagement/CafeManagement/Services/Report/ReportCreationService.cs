using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CafeManagement.Services.Report
{
    public class ReportCreationService : IReportCreationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportQueryService _reportQueryService;
        public ReportCreationService(IUnitOfWork unitOfWork, IReportQueryService reportQueryService)
        {
            _unitOfWork = unitOfWork;
            _reportQueryService = reportQueryService;
        }
        public async Task CreateDailyReport(DateTime date)
        {
            DateTime startTime = date.Date;
            DateTime endTime = date.Date.AddDays(1);
            DailyReport dailyReport = new DailyReport();
            dailyReport.Id = Guid.NewGuid();
            dailyReport.createDate = DateTime.Now;
            dailyReport.ReportDate = DateOnly.FromDateTime(date);
            dailyReport.TotalRevenue = await _reportQueryService.GetTotalRevenue(startTime, endTime);
            dailyReport.TotalExpenditure = await _reportQueryService.GetTotalExpenditure(startTime, endTime);
            dailyReport.NumberOfFinishedOrders = await _reportQueryService.GetNumberOfFinishedOrders(startTime, endTime);
            dailyReport.NumberOfCancelledOrders = await _reportQueryService.GetNumberOfCancelledOrders(startTime, endTime);
            dailyReport.TopSelling = await _reportQueryService.GetTopSellingProduct(startTime, endTime);
            dailyReport.LeastSelling = await _reportQueryService.GetLeastSellingProduct(startTime, endTime);
            dailyReport.PeakHours = await _reportQueryService.GetPeakHours(DateOnly.FromDateTime(date));
            dailyReport.TotalProductsSold = await _reportQueryService.GetTotalProductsSold(startTime, endTime);
            await _unitOfWork.DailyReport.Add(dailyReport);
        }

        public async Task CreateMonthlyReport(int month, int year)
        {
            MonthlyReport monthlyReport = new MonthlyReport();
            monthlyReport.Id = Guid.NewGuid();
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddSeconds(-1);
            var dailyReports = (await _unitOfWork.DailyReport.GetAll())
                .Where(dr => dr.ReportDate >= DateOnly.FromDateTime(startDate)
                 && dr.ReportDate <= DateOnly.FromDateTime(endDate))
                .ToList();
            if (dailyReports == null || !dailyReports.Any())
            {
                dailyReports = new List<DailyReport>();
            }
            monthlyReport.TotalRevenue = dailyReports.Sum(dr => dr.TotalRevenue);
            monthlyReport.TotalProductsSold = dailyReports.Sum(dr => dr.TotalProductsSold);
            monthlyReport.createDate = DateTime.Now;
            monthlyReport.StartDate = DateOnly.FromDateTime(startDate);
            monthlyReport.EndDate = DateOnly.FromDateTime(endDate);
            monthlyReport.TotalExpenditure = dailyReports.Sum(dr => dr.TotalExpenditure);
            monthlyReport.NumberOfFinishedOrders = dailyReports.Sum(dr => dr.NumberOfFinishedOrders);
            monthlyReport.NumberOfCancelledOrders = dailyReports.Sum(dr => dr.NumberOfCancelledOrders);
            monthlyReport.TopSelling = await _reportQueryService.GetTopSellingProduct(startDate, endDate);
            monthlyReport.LeastSelling = await _reportQueryService.GetLeastSellingProduct(startDate, endDate);
            await _unitOfWork.MonthlyReport.Add(monthlyReport);
        }

        public async Task CreateQuarterlyReport(int quarter, int year)
        {
            
            (DateTime startDate, DateTime endDate) = QuarterHelper.GetQuarterDates(quarter, year);
            int startMonth = (quarter - 1) * 3 + 1;
            int endMonth = startMonth + 2;
            var monthlyReports = (await _unitOfWork.MonthlyReport.GetAll())
               .Where(mr => mr.StartDate.Year == year
                         && mr.StartDate.Month >= startMonth
                         && mr.StartDate.Month <= endMonth)
               .ToList();
            if (monthlyReports == null || !monthlyReports.Any())
            {
                throw new Exception("Không có báo cáo tháng nào trong quý này.");
            }
            QuarterlyReport quarterlyReport = new QuarterlyReport
            {
                Id = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(startDate),
                EndDate = DateOnly.FromDateTime(endDate),
                TotalRevenue = monthlyReports.Sum(m => m.TotalRevenue),
                TotalProductsSold = monthlyReports.Sum(m => m.TotalProductsSold),
                TotalExpenditure = monthlyReports.Sum(m=>m.TotalExpenditure),
                NumberOfCancelledOrders = monthlyReports.Sum(m=>m.NumberOfCancelledOrders),
                TopSelling = await _reportQueryService.GetTopSellingProduct(startDate, endDate),
                LeastSelling = await _reportQueryService.GetLeastSellingProduct(startDate, endDate),
                NumberOfFinishedOrders = monthlyReports.Sum(m=>m.NumberOfFinishedOrders),
                MonthlyReports = monthlyReports,
                createDate = DateTime.Now
            };
        }
    }
}
