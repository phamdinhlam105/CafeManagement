using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Report
{
    public class ReportUpdateService : IReportUpdateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportQueryService _reportQueryService;
        private readonly IReportCreationService _reportCreationService;
        private readonly IReportRetrievalService _reportRetrievalService;

        public ReportUpdateService(IUnitOfWork unitOfWork, 
            IReportQueryService reportQueryService, 
            IReportCreationService reportCreationService, 
            IReportRetrievalService reportRetrievalService)
        {
            _unitOfWork = unitOfWork;
            _reportQueryService = reportQueryService;
            _reportCreationService = reportCreationService;
            _reportRetrievalService = reportRetrievalService;
        }

        public async Task UpdateDailyReport(DateOnly date)
        {
            var dailyReport = (await _reportRetrievalService.GetDailyReport(date)).Report as DailyReport;
            if (dailyReport.createDate > DateTime.UtcNow.AddHours(-1))
                return;
            DateTime startTime = date.ToDateTime(new TimeOnly(0, 0));
            DateTime endTime = startTime.AddDays(1);

            dailyReport.createDate = DateTime.UtcNow;
            dailyReport.TotalRevenue = await _reportQueryService.GetTotalRevenue(startTime, endTime);
            dailyReport.TotalExpenditure = await _reportQueryService.GetTotalExpenditure(startTime, endTime);
            dailyReport.NumberOfFinishedOrders = await _reportQueryService.GetNumberOfFinishedOrders(startTime, endTime);
            dailyReport.NumberOfCancelledOrders = await _reportQueryService.GetNumberOfCancelledOrders(startTime, endTime);
            dailyReport.TopSelling = await _reportQueryService.GetTopSellingProduct(startTime, endTime);
            dailyReport.LeastSelling = await _reportQueryService.GetLeastSellingProduct(startTime, endTime);
            dailyReport.PeakHours = await _reportQueryService.GetPeakHours(date);
            dailyReport.TotalProductsSold = await _reportQueryService.GetTotalProductsSold(startTime, endTime);
            try
            {
                await _unitOfWork.DailyReport.Update(dailyReport);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task UpdateMonthlyReport(int month, int year)
        {
            var startDate = new DateOnly(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var today = DateOnly.FromDateTime(DateTime.Today);
            if (endDate > today) endDate = today; 

            var monthlyReport =(await _reportRetrievalService.GetMonthlyReport(month, year)).Report as MonthlyReport;
            if (monthlyReport.createDate > DateTime.UtcNow.AddHours(-1))
                return;
            var dailyReports = monthlyReport.DailyReports.ToList();
            var existingDates = monthlyReport.DailyReports.Select(d => d.ReportDate).ToHashSet();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (!existingDates.Contains(date))
                {
                    var dailyReport = (await _reportRetrievalService.GetDailyReport(date)).Report as DailyReport;
                    dailyReports.Add(dailyReport);
                }
            }
            monthlyReport.DailyReports = dailyReports;
            monthlyReport.TotalRevenue = dailyReports.Sum(dr => dr.TotalRevenue);
            monthlyReport.TotalProductsSold = dailyReports.Sum(dr => dr.TotalProductsSold);
            monthlyReport.createDate = DateTime.UtcNow;
            monthlyReport.StartDate = startDate;
            monthlyReport.EndDate = endDate;
            monthlyReport.TotalExpenditure = dailyReports.Sum(dr => dr.TotalExpenditure);
            monthlyReport.NumberOfFinishedOrders = dailyReports.Sum(dr => dr.NumberOfFinishedOrders);
            monthlyReport.NumberOfCancelledOrders = dailyReports.Sum(dr => dr.NumberOfCancelledOrders);
            monthlyReport.TopSelling = await _reportQueryService.GetTopSellingProduct(startDate.ToDateTime(TimeOnly.MinValue), endDate.ToDateTime(TimeOnly.MaxValue));
            monthlyReport.LeastSelling = await _reportQueryService.GetLeastSellingProduct(startDate.ToDateTime(TimeOnly.MinValue), endDate.ToDateTime(TimeOnly.MaxValue));

            try
            {
                await _unitOfWork.MonthlyReport.Update(monthlyReport);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateQuarterlyReport(int quarter, int year)
        {
            (DateOnly startDate, DateOnly endDate) = QuarterHelper.GetQuarterDates(year, quarter);

            var today = DateOnly.FromDateTime(DateTime.Today);
            if (endDate > today) endDate = today;

            var quarterlyReport = (await _reportRetrievalService.GetQuarterlyReport(quarter, year)).Report as QuarterlyReport;
            if (quarterlyReport.createDate > DateTime.UtcNow.AddHours(-1))
                return;

            var monthlyReports = quarterlyReport.MonthlyReports.ToList();
            var existingMonths = quarterlyReport.MonthlyReports.Select(mr => mr.StartDate.Month).ToHashSet();

            for (int month = startDate.Month; month <= endDate.Month; month++)
            {
                if (!existingMonths.Contains(month))
                {
                    var monthlyReport = (await _reportRetrievalService.GetMonthlyReport(month, year)).Report as MonthlyReport;
                    monthlyReports.Add(monthlyReport);
                }
            }
            quarterlyReport.MonthlyReports = monthlyReports;

            quarterlyReport.TotalRevenue = monthlyReports.Sum(mr => mr.TotalRevenue);
            quarterlyReport.TotalProductsSold = monthlyReports.Sum(mr => mr.TotalProductsSold);
            quarterlyReport.createDate = DateTime.UtcNow;
            quarterlyReport.StartDate = startDate;
            quarterlyReport.EndDate = endDate;
            quarterlyReport.TotalExpenditure = monthlyReports.Sum(mr => mr.TotalExpenditure);
            quarterlyReport.NumberOfFinishedOrders = monthlyReports.Sum(mr => mr.NumberOfFinishedOrders);
            quarterlyReport.NumberOfCancelledOrders = monthlyReports.Sum(mr => mr.NumberOfCancelledOrders);
            quarterlyReport.TopSelling = await _reportQueryService.GetTopSellingProduct(startDate.ToDateTime(TimeOnly.MinValue), endDate.ToDateTime(TimeOnly.MaxValue));
            quarterlyReport.LeastSelling = await _reportQueryService.GetLeastSellingProduct(startDate.ToDateTime(TimeOnly.MinValue), endDate.ToDateTime(TimeOnly.MaxValue));

            // Cập nhật vào DB
            try
            {
                await _unitOfWork.QuarterlyReport.Update(quarterlyReport);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
