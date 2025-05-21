using CafeManagement.Models.ProductModel;

namespace CafeManagement.Models.Report
{
    public class ProductReport
    {
        public Guid Id { get; set; }
        public Guid DailyReportId {  get; set; }
        public Guid ProductId { get; set; }
        public int QuantitySold {  get; set; }
        public DailyReport DailyReport { get; set; }
        public Product Product { get; set; }
    }
}
