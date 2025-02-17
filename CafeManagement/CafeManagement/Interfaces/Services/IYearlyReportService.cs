using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services
{
    public interface IYearlyReportService
    {
        YearlyReport CreateYearlyReport(int year);
        YearlyReport GetYearlyReport(int year);
        IEnumerable<YearlyReport> GetAllYearlyReports();
        void UpdateYearlyReport(int year, YearlyReportUpdateDto updateData);
        YearlyReportAnalysisDto AnalyzeYearlyReport(int year);
    }
}
