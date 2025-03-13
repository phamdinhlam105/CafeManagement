using CafeManagement.Data;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.Report
{
    public class QuarterlyReportRepository: BaseRepository<QuarterlyReport>, IQuarterlyReportRepository
    {
        public QuarterlyReportRepository(CafeManagementDbContext _context) : base(_context) { }
        public override async Task<IEnumerable<QuarterlyReport>> GetAll()
        {
            return await _context.QuarterlyReports
                .Include(qr => qr.MonthlyReports)
                .ThenInclude(mr=>mr.DailyReports)
                .ToListAsync();
        }
        public async Task<QuarterlyReport> GetByQuarter(int quarter,int year)
        {
            (DateOnly startDate, DateOnly endDate) = QuarterHelper.GetQuarterDates(year,quarter);
            return await _context.QuarterlyReports
                 .Include(qr => qr.MonthlyReports)
                .ThenInclude(mr => mr.DailyReports)
                .ThenInclude(dr=>dr.TopSelling)
                .Include(qr => qr.MonthlyReports)
                .ThenInclude(mr => mr.DailyReports)
                .ThenInclude(dr => dr.LeastSelling)
                .FirstOrDefaultAsync(qr => qr.StartDate == startDate && qr.EndDate == endDate);
        }
    }
}
