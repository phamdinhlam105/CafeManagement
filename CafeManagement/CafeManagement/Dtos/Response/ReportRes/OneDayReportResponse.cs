using CafeManagement.Helpers;
using CafeManagement.Models;
using Newtonsoft.Json;

namespace CafeManagement.Dtos.Respone.ReportRes
{
    public class OneDayReportResponse
    {
        public Guid Id { get; set; }
        public decimal TotalRevenue { get; set; }
        public string? TopSellingProduct { get; set; }
        public string? LeastSellingProduct { get; set; }
        public int NumberOfFinishedOrders { get; set; }
        public int NumberOfCancelledOrders { get; set; }
        public int TotalProductsSold { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? ReportDate { get; set; }
        public decimal TotalExpenditure { get; set; }
        public int NumberOfImports { get; set; }
    }
}
