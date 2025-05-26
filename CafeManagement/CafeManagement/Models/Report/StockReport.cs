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
        public Guid? AdjustmentId {  get; set; }

        public List<StockReportDetail> StockReportDetails {  get; set; }
        public StockReport()
        {
            StockReportDetails = new List<StockReportDetail>();
        }
    }
}
