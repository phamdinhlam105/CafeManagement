namespace CafeManagement.Interfaces.Services.Report
{
    public interface IReportUpdateService
    {
        Task UpdateDailyReport(DateOnly date);
        Task UpdateMonthlyReport(int month, int year);
        Task UpdateQuarterlyReport(int quarter, int year);
    }
}
