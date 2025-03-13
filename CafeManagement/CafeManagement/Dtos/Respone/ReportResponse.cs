using CafeManagement.Models.Report;

namespace CafeManagement.Dtos.Respone
{
    public class ReportResponse
    {
        public IEnumerable<ReportBase> Reports { get; set; }
        public IEnumerable<BestDays>? BestDays { get; set; }

    }
}
