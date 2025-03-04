using CafeManagement.Models.Report;

namespace CafeManagement.Dtos.Respone
{
    public class ReportResponse
    {
        public ReportBase Report { get; set; }
        public IEnumerable<BestDays>? BestDays { get; set; }

    }
}
