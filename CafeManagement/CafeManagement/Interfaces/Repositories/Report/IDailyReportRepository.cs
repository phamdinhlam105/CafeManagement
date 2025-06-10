using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Repositories.Report
{
    public interface IDailyReportRepository:IRepository<DailyReport>
    {
        Task<DailyReport> GetByDate(DateOnly date);
        Task<List<DailyReport>> GetByDateRange(DateOnly start, DateOnly end);
        Task<DailyReport> GetLastestReport();
    }
}
