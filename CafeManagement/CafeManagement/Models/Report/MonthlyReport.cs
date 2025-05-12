using CafeManagement.Helpers;
using System.Text.Json.Serialization;

namespace CafeManagement.Models.Report
{
    public class MonthlyReport : ReportBase
    {
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly StartDate { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly EndDate { get; set; }
        public Guid? QuarterlyReportId {  get; set; }
        public IEnumerable<DailyReport> DailyReports { get; set; }

        public MonthlyReport()
        {
            DailyReports = new List<DailyReport>();
        }
    }
}
