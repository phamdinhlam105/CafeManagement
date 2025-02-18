using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IReportRetrievalService
    {
        DailyReport? GetDailyReport(DateOnly date);
        MonthlyReport? GetMonthlyReport(int month, int year);
        QuarterlyReport? GetQuarterlyReport(int quarter, int year);
        IEnumerable<DailyReport> GetReportsByRange(DateOnly startDate, DateOnly endDate);
    }
}
