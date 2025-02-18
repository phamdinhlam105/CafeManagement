using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;

namespace CafeManagement.Repositories.Report
{
    public class DailyReportRepository:BaseRepository<DailyReport>,IDailyReportRepository
    {
        public DailyReportRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
