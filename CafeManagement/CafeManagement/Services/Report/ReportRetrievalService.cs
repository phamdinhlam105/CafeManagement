using CafeManagement.Dtos.Respone;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace CafeManagement.Services.Report
{
    public class ReportRetrievalService : IReportRetrievalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportQueryService _reportQueryService;
        private readonly IReportCreationService _reportCreationService;
        public ReportRetrievalService(IUnitOfWork unitOfWork , IReportQueryService reportQueryService, IReportCreationService reportCreationService)
        {
            _unitOfWork = unitOfWork;
            _reportQueryService = reportQueryService;
            _reportCreationService = reportCreationService;
        }
        public async Task<ReportResponse?> GetDailyReport(DateOnly date)
        {

            var report = await _unitOfWork.DailyReport.GetByDate(date);

            if (report == null)
            {
                await _reportCreationService.CreateDailyReport(date);

                report = await _unitOfWork.DailyReport.GetByDate(date);
            }
            var response =  new ReportResponse { Reports = new List<OneDayReportResponse>() };
            response.Reports.Add(new OneDayReportResponse
            {
                Id=report.Id,
                TotalRevenue=report.TotalRevenue,
                TotalExpenditure=report.TotalExpenditure,
                TopSellingId = report.TopSellingId,
                LeastSellingId=report.LeastSellingId,
                NumberOfCancelledOrders=report.NumberOfCancelledOrders,
                NumberOfFinishedOrders=report.NumberOfFinishedOrders,
                TotalProductsSold=report.TotalProductsSold,
                CreateDate=report.createDate,
                ReportDate=report.ReportDate,
                PeakHours=report.PeakHours,
            });
            return response;
        }

        public async Task<ReportResponse?> GetMonthlyReport(int month, int year)
        {
            var report = await _unitOfWork.MonthlyReport.GetByMonth(month, year);

            if (report == null)
            {
                await _reportCreationService.CreateMonthlyReport(month, year);
                report = await _unitOfWork.MonthlyReport.GetByMonth(month,year);
            }

            var startDateTime = new DateTime(year, month, 1);
            var endDateTime = startDateTime.AddMonths(1).AddSeconds(-1);

            var reports = new List<MonthlyReport>();
            reports.Add(report);
            var response = new ReportResponse
            {
                Reports = new List<OneDayReportResponse>(),
                BestDays = (await _reportQueryService.GetBestDaysInWeek(startDateTime, endDateTime)).ToList()
            };
            response.Reports.Add(new OneDayReportResponse
            {
                Id = report.Id,
                TotalRevenue = report.TotalRevenue,
                TotalExpenditure = report.TotalExpenditure,
                TopSellingId = report.TopSellingId,
                LeastSellingId = report.LeastSellingId,
                NumberOfCancelledOrders = report.NumberOfCancelledOrders,
                NumberOfFinishedOrders = report.NumberOfFinishedOrders,
                TotalProductsSold = report.TotalProductsSold,
                CreateDate = report.createDate,
                StartDate = report.StartDate,
                EndDate = report.EndDate,
            });
            return response;
        }

        public async Task<ReportResponse?> GetQuarterlyReport(int quarter, int year)
        {
            var (startDateTime, endDateTime) = QuarterHelper.GetQuarterDates(year, quarter);
            var startDate = startDateTime.ToDateTime(new TimeOnly(0, 0));
            var endDate = endDateTime.ToDateTime(new TimeOnly(23, 59, 59));

            var report = await _unitOfWork.QuarterlyReport.GetByQuarter(quarter, year);

            if (report == null)
            {
                await _reportCreationService.CreateQuarterlyReport(quarter, year);
                report = await _unitOfWork.QuarterlyReport.GetByQuarter(quarter,year);
            }

            var response =  new ReportResponse
            {
                Reports = new List<OneDayReportResponse>(),
                BestDays = (await _reportQueryService.GetBestDaysInWeek(startDate, endDate)).ToList()
            };
            response.Reports.Add(new OneDayReportResponse
            {
                Id = report.Id,
                TotalRevenue = report.TotalRevenue,
                TotalExpenditure = report.TotalExpenditure,
                TopSellingId = report.TopSellingId,
                LeastSellingId = report.LeastSellingId,
                NumberOfCancelledOrders = report.NumberOfCancelledOrders,
                NumberOfFinishedOrders = report.NumberOfFinishedOrders,
                TotalProductsSold = report.TotalProductsSold,
                CreateDate = report.createDate,
                StartDate = report.StartDate,
                EndDate = report.EndDate,
            });
            return response;
        }

        public async Task<ReportResponse> GetReportsByRange(DateOnly startDate, DateOnly endDate)
        {
            var dailyReports = await _unitOfWork.DailyReport.GetByDateRange(startDate, endDate);
            if (!dailyReports.Any())
            {
                var currentDate = startDate;
                while (currentDate <= endDate)
                {
                    await _reportCreationService.CreateDailyReport(currentDate);
                    currentDate = currentDate.AddDays(1);
                }

                dailyReports = await _unitOfWork.DailyReport.GetByDateRange(startDate, endDate);
            }
            var startDateTime = startDate.ToDateTime(new TimeOnly(0, 0),DateTimeKind.Utc);
            var endDateTime = endDate.ToDateTime(new TimeOnly(23, 59, 59), DateTimeKind.Utc);
            var response = new ReportResponse
            {
                Reports = new List<OneDayReportResponse>(),
                BestDays = (await _reportQueryService.GetBestDaysInWeek(startDateTime, endDateTime)).ToList()
            };
            foreach (var report in dailyReports)
            {
                response.Reports.Add(new OneDayReportResponse
                {
                    Id = report.Id,
                    TotalRevenue = report.TotalRevenue,
                    TotalExpenditure = report.TotalExpenditure,
                    TopSellingId = report.TopSellingId,
                    LeastSellingId = report.LeastSellingId,
                    NumberOfCancelledOrders = report.NumberOfCancelledOrders,
                    NumberOfFinishedOrders = report.NumberOfFinishedOrders,
                    TotalProductsSold = report.TotalProductsSold,
                    CreateDate = report.createDate,
                    ReportDate = report.ReportDate,
                    PeakHours = report.PeakHours,
                });
            }

            return response;
        }
    }
}
