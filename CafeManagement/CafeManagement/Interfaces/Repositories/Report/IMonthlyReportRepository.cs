using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Repositories.Report
{
    public interface IMonthlyReportRepository:IRepository<MonthlyReport>
    {
        Task<MonthlyReport> GetByMonth(int month, int year);
    }
}
