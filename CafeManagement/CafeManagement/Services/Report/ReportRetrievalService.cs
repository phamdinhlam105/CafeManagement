using CafeManagement.Dtos.Respone;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            var response =  new ReportResponse { Reports = new List<DailyReport>() };
            response.Reports.Append(report); 
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

            var response =  new ReportResponse
            {
                Reports = new List<MonthlyReport>(),
                BestDays = await _reportQueryService.GetBestDaysInWeek(startDateTime, endDateTime)
            };
            response.Reports.Append(report);
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

            return new ReportResponse
            {
                Reports = new List<QuarterlyReport>().Append(report),
                BestDays = await _reportQueryService.GetBestDaysInWeek(startDate, endDate)
            };
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
            var startDateTime = startDate.ToDateTime(new TimeOnly(0, 0));
            var endDateTime = endDate.ToDateTime(new TimeOnly(23, 59, 59));
            var response = new ReportResponse
            {
                Reports = new List<DailyReport>(),
                BestDays = await _reportQueryService.GetBestDaysInWeek(startDateTime, endDateTime)
            };
            foreach (var dailyReport in dailyReports)
            {
                response.Reports.Append(dailyReport);
            }

            return response;
        }
    }
}
