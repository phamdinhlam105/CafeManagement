namespace CafeManagement.Models
{
    public class YearlyReport:ReportBase
    {
        public List<Customer> TopLoyalCustomers { get; set; }
        public List<DailyReport> BestDaysInYear { get; set; }
        public List<QuarterlyReport> QuarterlyReports { get; set; }

        public YearlyReport()
        {
            TopLoyalCustomers = new List<Customer>();
            BestDaysInYear = new List<DailyReport>();
            QuarterlyReports= new List<QuarterlyReport>();
        }
    }
}
