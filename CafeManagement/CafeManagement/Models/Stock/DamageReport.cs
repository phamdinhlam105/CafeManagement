namespace CafeManagement.Models.Stock
{
    public class DamageReport
    {
        public Guid Id { get; set; }

        public DateTime ReportDate { get; set; } = DateTime.UtcNow;

        public string ReportedBy { get; set; }  // Người báo cáo hư hại

        public string DamageType { get; set; }  // Ví dụ: "Broken", "Expired", "Lost"

        public string Description { get; set; }  // Mô tả chi tiết

        public decimal EstimatedLossAmount { get; set; }  // Tổn thất tài chính ước tính

        public ICollection<DamageDetail> DamageDetails { get; set; } = new List<DamageDetail>();
    }
}
