namespace CafeManagement.Models.Report
{
    public class DailyReport : ReportBase
    {
        public DateOnly ReportDate { get; set; }
        public List<TimeSpan> PeakHours { get; set; }
    }
}
