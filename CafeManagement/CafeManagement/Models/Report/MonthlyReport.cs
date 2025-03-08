namespace CafeManagement.Models.Report
{
    public class MonthlyReport : ReportBase
    {

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Guid QuarterlyReportId {  get; set; }
        public IEnumerable<DailyReport> DailyReports { get; set; }

        public MonthlyReport()
        {
            DailyReports = new List<DailyReport>();
        }
    }
}
