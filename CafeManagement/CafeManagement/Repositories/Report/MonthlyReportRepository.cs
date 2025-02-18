using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;

namespace CafeManagement.Repositories.Report
{
    public class MonthlyReportRepository : BaseRepository<MonthlyReport>, IMonthlyReportRepository
    {
        public MonthlyReportRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
