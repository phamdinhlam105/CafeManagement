namespace CafeManagement.Models.Report
{
    public class OrderReport
    {
        public Guid Id {  get; set; }
        public Guid DailyReportId {  get; set; }
        public DailyReport DailyReport { get; set; }
        public decimal TotalRevenue { get; set; }
        public Product? TopSelling { get; set; }
        public Guid? TopSellingId { get; set; }
        public Product? LeastSelling { get; set; }
        public Guid? LeastSellingId { get; set; }
        public int NumberOfFinishedOrders { get; set; }
        public int NumberOfCancelledOrders { get; set; }
        public int TotalProductsSold { get; set; }
    }
}
