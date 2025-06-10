namespace CafeManagement.Models.Stock
{
    public class DamageDetail
    {
        public Guid Id { get; set; }

        public Guid DamageReportId { get; set; }
        public DamageReport DamageReport { get; set; }

        public Guid ProductId { get; set; }  // Sản phẩm hoặc nguyên liệu bị hư hại

        public decimal Quantity { get; set; }  // Số lượng hư hại

        public decimal LossValue { get; set; }  // Giá trị tổn thất tương ứng
    }
}
