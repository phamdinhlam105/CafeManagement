using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IReportCreationService
    {
        Task<DailyReport> GetTodayReport();
    }
}
