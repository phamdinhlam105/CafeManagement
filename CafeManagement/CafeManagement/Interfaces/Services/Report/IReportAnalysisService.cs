namespace CafeManagement.Interfaces.Services.Report
{
    public interface IReportAnalysisService
    {
        ReportAnalysisDto AnalyzeDailyReport(DateOnly date);
        ReportAnalysisDto AnalyzeMonthlyReport(int month, int year);
        ReportAnalysisDto AnalyzeQuarterlyReport(int quarter, int year);
    }
}
