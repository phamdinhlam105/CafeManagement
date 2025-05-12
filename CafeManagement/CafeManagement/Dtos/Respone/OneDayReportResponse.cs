using CafeManagement.Helpers;
using CafeManagement.Models;
using Newtonsoft.Json;

namespace CafeManagement.Dtos.Respone
{
    public class OneDayReportResponse
    {
        public Guid Id { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenditure { get; set; }
        public Guid? TopSellingId { get; set; }
        public Guid? LeastSellingId { get; set; }
        public int NumberOfFinishedOrders { get; set; }
        public int NumberOfCancelledOrders { get; set; }
        public int TotalProductsSold { get; set; }
        public DateTime CreateDate { get; set; }
        public DateOnly? ReportDate {  get; set; }
        public List<int>? PeakHours { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? StartDate { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? EndDate { get; set; }

    }
}
