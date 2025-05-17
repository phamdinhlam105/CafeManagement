using CafeManagement.Models.Report;

namespace CafeManagement.Dtos.Respone.ReportRes
{
    public class ReportResponse
    {
        public ICollection<OneDayReportResponse> Reports { get; set; }
        public ICollection<BestDays>? BestDays { get; set; }

        public ReportResponse()
        {
            Reports = new List<OneDayReportResponse>();

        }
    }
}
