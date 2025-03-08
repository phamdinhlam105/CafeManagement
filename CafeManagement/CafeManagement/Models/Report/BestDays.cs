namespace CafeManagement.Models.Report
{
    public class BestDays
    {
        public Guid Id { get; set; }
        public string WeekDay { get; set; }
        public decimal AvgRevenue { get; set; }
    }
}
