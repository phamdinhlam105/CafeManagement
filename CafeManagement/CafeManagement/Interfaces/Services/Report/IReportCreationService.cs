using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IReportCreationService
    {
        Task CreateDailyReport(DateTime date);
        Task CreateMonthlyReport(int month, int year);
        Task CreateQuarterlyReport(int quarter, int year);
    }
}
