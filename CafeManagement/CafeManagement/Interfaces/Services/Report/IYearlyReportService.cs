using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IYearlyReportService
    {
        YearlyReport CreateYearlyReport(int year);
        YearlyReport GetYearlyReport(int year);
        IEnumerable<YearlyReport> GetAllYearlyReports();
    }
}
