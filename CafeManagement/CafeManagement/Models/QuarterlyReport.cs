namespace CafeManagement.Models
{
    public class QuarterlyReport:ReportBase
    {
        public int Quarter {  get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public IEnumerable<DailyReport> BestDaysInQuarter { get; set; }

        public QuarterlyReport()
        {
            BestDaysInQuarter = new List<DailyReport>();
        }
    }
}
