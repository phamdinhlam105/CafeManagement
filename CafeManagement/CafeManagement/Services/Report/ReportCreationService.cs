using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;
using Microsoft.Extensions.Configuration.UserSecrets;
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
        public async Task CreateDailyReport(DateOnly date)
        {
            DateTime startTime = date.ToDateTime(new TimeOnly(0, 0));
            DateTime endTime = startTime.AddDays(1);
            DailyReport dailyReport = new DailyReport();
            dailyReport.Id = Guid.NewGuid();
            dailyReport.createDate = DateTime.UtcNow;
            dailyReport.ReportDate = date;
            dailyReport.TotalRevenue = await _reportQueryService.GetTotalRevenue(startTime, endTime);
            dailyReport.TotalExpenditure = await _reportQueryService.GetTotalExpenditure(startTime, endTime);
            dailyReport.NumberOfFinishedOrders = await _reportQueryService.GetNumberOfFinishedOrders(startTime, endTime);
            dailyReport.NumberOfCancelledOrders = await _reportQueryService.GetNumberOfCancelledOrders(startTime, endTime);
            dailyReport.TopSelling = await _reportQueryService.GetTopSellingProduct(startTime, endTime);
            dailyReport.LeastSelling = await _reportQueryService.GetLeastSellingProduct(startTime, endTime);
            dailyReport.PeakHours = await _reportQueryService.GetPeakHours(date);
            dailyReport.TotalProductsSold = await _reportQueryService.GetTotalProductsSold(startTime, endTime);
            await _unitOfWork.DailyReport.Add(dailyReport);
        }

        public async Task CreateMonthlyReport(int month, int year)
        {
            MonthlyReport monthlyReport = new MonthlyReport();
            monthlyReport.Id = Guid.NewGuid();
            DateOnly startDate = new DateOnly(year, month, 1);
            DateOnly endDate = startDate.AddMonths(1).AddDays(-1);
            DateOnly today = Ultilities.GetToday();
            DateOnly lastDate = endDate < today ? endDate : today;
            DateTime startDateTime = startDate.ToDateTime(new TimeOnly(0, 0));
            DateTime lastDateTime = lastDate.ToDateTime(new TimeOnly(23, 59, 59));
            var dailyReports = (await _unitOfWork.DailyReport.GetAll())
                .Where(dr => dr.ReportDate >= startDate && dr.ReportDate <= lastDate)
                .ToList();


            if (dailyReports == null || !dailyReports.Any())
            {
                for (DateOnly date = startDate; date <= lastDate; date = date.AddDays(1))
                {
                    await CreateDailyReport(date);
                }

                dailyReports = (await _unitOfWork.DailyReport.GetAll())
                    .Where(dr => dr.ReportDate >= startDate && dr.ReportDate <= lastDate)
                    .ToList();
            }

            monthlyReport.TotalRevenue = dailyReports.Sum(dr => dr.TotalRevenue);
            monthlyReport.TotalProductsSold = dailyReports.Sum(dr => dr.TotalProductsSold);
            monthlyReport.createDate = DateTime.UtcNow;
            monthlyReport.StartDate = startDate;
            monthlyReport.EndDate = endDate;
            monthlyReport.TotalExpenditure = dailyReports.Sum(dr => dr.TotalExpenditure);
            monthlyReport.NumberOfFinishedOrders = dailyReports.Sum(dr => dr.NumberOfFinishedOrders);
            monthlyReport.NumberOfCancelledOrders = dailyReports.Sum(dr => dr.NumberOfCancelledOrders);
            monthlyReport.TopSelling = await _reportQueryService.GetTopSellingProduct(startDateTime, lastDateTime);
            monthlyReport.LeastSelling = await _reportQueryService.GetLeastSellingProduct(startDateTime, lastDateTime);

            await _unitOfWork.MonthlyReport.Add(monthlyReport);
        }

        public async Task CreateQuarterlyReport(int quarter, int year)
        {

            (DateOnly startDate, DateOnly endDate) = QuarterHelper.GetQuarterDates(quarter, year);
            DateOnly today = Ultilities.GetToday();
            DateOnly lastDate = endDate < today ? endDate : today;

            int startMonth = (quarter - 1) * 3 + 1;
            int endMonth = startMonth + 2;
            DateTime startDateTime = startDate.ToDateTime(new TimeOnly(0, 0));
            DateTime lastDateTime = lastDate.ToDateTime(new TimeOnly(23, 59, 59));
            var monthlyReports = (await _unitOfWork.MonthlyReport.GetAll())
                .Where(mr => mr.StartDate.Year == year
                          && mr.StartDate.Month >= startMonth
                          && mr.StartDate.Month <= endMonth)
                .ToList();

            // Nếu chưa có báo cáo tháng nào => Tạo báo cáo tháng từ startDate đến lastDate
            if (monthlyReports == null || !monthlyReports.Any())
            {
                for (int month = startMonth; month <= endMonth && new DateOnly(year, month, 1) <= lastDate; month++)
                {
                    await CreateMonthlyReport(month, year);
                }

                // Load lại danh sách báo cáo tháng sau khi tạo
                monthlyReports = (await _unitOfWork.MonthlyReport.GetAll())
                    .Where(mr => mr.StartDate.Year == year
                              && mr.StartDate.Month >= startMonth
                              && mr.StartDate.Month <= endMonth)
                    .ToList();
            }

            if (monthlyReports == null || !monthlyReports.Any())
            {
                throw new Exception("Không có báo cáo tháng nào trong quý này.");
            }

            QuarterlyReport quarterlyReport = new QuarterlyReport
            {
                Id = Guid.NewGuid(),
                StartDate = startDate,
                EndDate = endDate,
                TotalRevenue = monthlyReports.Sum(m => m.TotalRevenue),
                TotalProductsSold = monthlyReports.Sum(m => m.TotalProductsSold),
                TotalExpenditure = monthlyReports.Sum(m => m.TotalExpenditure),
                NumberOfCancelledOrders = monthlyReports.Sum(m => m.NumberOfCancelledOrders),
                TopSelling = await _reportQueryService.GetTopSellingProduct(startDateTime, lastDateTime),
                LeastSelling = await _reportQueryService.GetLeastSellingProduct(startDateTime, lastDateTime),
                NumberOfFinishedOrders = monthlyReports.Sum(m => m.NumberOfFinishedOrders),
                MonthlyReports = monthlyReports,
                createDate = DateTime.UtcNow
            };

            await _unitOfWork.QuarterlyReport.Add(quarterlyReport);
        }
    }
}
