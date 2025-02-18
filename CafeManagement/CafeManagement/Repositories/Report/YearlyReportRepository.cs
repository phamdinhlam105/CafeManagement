using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;

namespace CafeManagement.Repositories.Report
{
    public class YearlyReportRepository:BaseRepository<YearlyReport>,IYearlyReportRepository
    {
        public YearlyReportRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
