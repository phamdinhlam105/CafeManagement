using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IYearlyReportService
    {
        Task<YearlyReport> CreateYearlyReport(int year);
        Task<YearlyReport> GetYearlyReport(int year);
        Task<IEnumerable<YearlyReport>> GetAllYearlyReports();
    }
}
