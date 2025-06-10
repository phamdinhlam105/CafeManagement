using CafeManagement.Dtos.Respone.ReportRes;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Report
{
    public class ReportRetrievalService : IReportRetrievalService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportRetrievalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DailyReport?> GetDailyReport(DateOnly date)
        {
            var todayReport = await _unitOfWork.DailyReport.GetByDate(date);
            if (todayReport == null)
                return null;
         
            return todayReport;
        }

        public async Task<ReportResponse?> GetMonthlyReport(int month, int year)
        {
            return null;
        }

        public async Task<ReportResponse?> GetQuarterlyReport(int quarter, int year)
        {
            return null;
        }

        public async Task<List<DailyReport>> GetReportsByRange(DateOnly startDate, DateOnly endDate)
        {
            var dailyReportList = (await _unitOfWork.DailyReport.GetByDateRange(startDate, endDate)).ToList();
            foreach (var dailyReport in dailyReportList)
            {
            }
            return dailyReportList;
        }
    }
}
