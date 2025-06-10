

using CafeManagement.Models.OrderModel;

namespace CafeManagement.Models.Report
{
    public class YearlyReport
    {
        public List<Customer> TopLoyalCustomers { get; set; }
        public List<QuarterlyReport> QuarterlyReports { get; set; }
        public int Year {  get; set; }
        public YearlyReport()
        {
            TopLoyalCustomers = new List<Customer>();
            QuarterlyReports = new List<QuarterlyReport>();
        }
    }
}
