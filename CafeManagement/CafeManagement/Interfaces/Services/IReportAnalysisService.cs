namespace CafeManagement.Interfaces.Services
{
    public interface IReportAnalysisService
    {
        ReportAnalysisDto AnalyzeDailyReport(DateOnly date);
        ReportAnalysisDto AnalyzeMonthlyReport(int month, int year);
        ReportAnalysisDto AnalyzeQuarterlyReport(int quarter, int year);
    }
}
