using CafeManagement.Dtos.Respone.ReportRes;
using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IReportRetrievalService
    {
        Task<DailyReport?> GetDailyReport(DateOnly date);
        Task<ReportResponse?> GetMonthlyReport(int month, int year);
        Task<ReportResponse?> GetQuarterlyReport(int quarter, int year);
        Task<List<DailyReport>> GetReportsByRange(DateOnly startDate, DateOnly endDate);
    }
}
