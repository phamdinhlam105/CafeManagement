using CafeManagement.Helpers;
using System.Text.Json.Serialization;

namespace CafeManagement.Models.Report
{
    public class QuarterlyReport : ReportBase
    {
        public int Quarter { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly StartDate { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly EndDate { get; set; }
        public IEnumerable<MonthlyReport> MonthlyReports { get; set; }

        public QuarterlyReport()
        {
            MonthlyReports = new List<MonthlyReport>();
        }
    }
}
