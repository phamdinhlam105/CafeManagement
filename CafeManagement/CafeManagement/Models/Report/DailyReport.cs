using CafeManagement.Helpers;
using System.Text.Json.Serialization;

namespace CafeManagement.Models.Report
{
    public class DailyReport : ReportBase
    {
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly ReportDate { get; set; }
        public Guid? MonthlyReportId {  get; set; }
        public List<int> PeakHours { get; set; }
    }
}
