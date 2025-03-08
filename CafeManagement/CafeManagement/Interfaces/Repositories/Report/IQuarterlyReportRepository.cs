using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Repositories.Report
{
    public interface IQuarterlyReportRepository:IRepository<QuarterlyReport>
    {
        Task<QuarterlyReport> GetByQuarter(int quarter, int year);
    }
}
