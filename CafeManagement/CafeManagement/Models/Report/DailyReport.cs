using CafeManagement.Helpers;
using System.Text.Json.Serialization;

namespace CafeManagement.Models.Report
{
    public class DailyReport
    {
        public Guid Id { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly ReportDate { get; set; }
        public Guid OrderReportId {  get; set; }
        public Guid StockReportId {  get; set; }
        public bool IsOrderReportUpToDate {  get; set; }
        public bool IsStockReportUpToDate { get; set; }
        public OrderReport OrderReport { get; set; }
        public StockReport StockReport { get; set; }
        public ICollection<ProductReport> ProductReports {  get; set; }
        public DailyReport()
        {
            ProductReports = new List<ProductReport>();
            IsOrderReportUpToDate = true;
            IsStockReportUpToDate = true;
        }
    }
}
