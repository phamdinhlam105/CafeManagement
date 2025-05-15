using CafeManagement.Models.Stock;

namespace CafeManagement.Models.Report
{
    public class StockReport
    {
        public Guid Id { get; set; }
        public Guid DailyReportId { get; set; }
        public DailyReport DailyReport {  get; set; }
        public decimal TotalExpenditure { get; set; }
        public int NumberOfImports {  get; set; }
        public ICollection<StockEntry> StockEntries { get; set; }
    }
}
