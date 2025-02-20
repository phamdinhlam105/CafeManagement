namespace CafeManagement.Models.Report
{
    public class DailyReport : ReportBase
    {
        public DateOnly ReportDate { get; set; }
        public List<int> PeakHours { get; set; }
    }
}
