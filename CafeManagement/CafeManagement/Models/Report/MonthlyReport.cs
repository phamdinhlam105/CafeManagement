namespace CafeManagement.Models.Report
{
    public class MonthlyReport : ReportBase
    {

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public IEnumerable<BestSellingDayInWeek> BestSellingDaysInWeek { get; set; }
        public IEnumerable<DailyReport> DailyReports { get; set; }

        public MonthlyReport()
        {
            BestSellingDaysInWeek = new List<BestSellingDayInWeek>();
            DailyReports = new List<DailyReport>();
        }
    }
}
