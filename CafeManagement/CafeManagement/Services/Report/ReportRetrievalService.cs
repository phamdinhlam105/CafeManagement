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
        private IUnitOfWork _unitOfWork;
        private IReportQueryService _reportQueryService;
        public ReportRetrievalService(IUnitOfWork unitOfWork , IReportQueryService reportQueryService)
        {
            _unitOfWork = unitOfWork;
            _reportQueryService = reportQueryService;
        }
        public async Task<ReportResponse?> GetDailyReport(DateOnly date)
        {
            return new ReportResponse{
                Report = (await _unitOfWork.DailyReport.GetAll())
               .FirstOrDefault(dr => dr.createDate.Equals(date))
            };
        }

        public async Task<ReportResponse?> GetMonthlyReport(int month, int year)
        {
            return new ReportResponse
            {
                Report = (await _unitOfWork.MonthlyReport.GetAll())
            .FirstOrDefault(mr => mr.StartDate.Year == year && mr.StartDate.Month == month),

            };
        }

        public async Task<ReportResponse?> GetQuarterlyReport(int quarter, int year)
        {
            var (startDateTime, endDateTime) = QuarterHelper.GetQuarterDates(year, quarter);
            var startDate = DateOnly.FromDateTime(startDateTime);
            var endDate = DateOnly.FromDateTime(endDateTime);
            return new ReportResponse
            {
                Report = (await _unitOfWork.QuarterlyReport.GetAll())
                .FirstOrDefault(qr => qr.StartDate >= startDate && qr.EndDate <= endDate)
            };
        }

        public async Task<IEnumerable<ReportResponse>> GetReportsByRange(DateOnly startDate, DateOnly endDate)
        {
            var response = new List<ReportResponse>();
            var dailyreports = (await _unitOfWork.DailyReport.GetAll())
               .Where(dr => dr.createDate.CompareTo(startDate) >= 0 && dr.createDate.CompareTo(endDate) <= 0)
               .ToList();
            foreach(var dailyreport in dailyreports)
                response.Add(new ReportResponse { Report=dailyreport });
            return response;
        }
    }
}
