using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Repositories.Report
{
    public interface IDailyReportRepository:IRepository<DailyReport>
    {
        Task<DailyReport> GetByDate(DateOnly date);
        Task<IEnumerable<DailyReport>> GetByDateRange(DateOnly start, DateOnly end);
    }
}
