using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;

namespace CafeManagement.Repositories.Report
{
    public class QuarterlyReportRepository: BaseRepository<QuarterlyReport>, IQuarterlyReportRepository
    {
        public QuarterlyReportRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
