using CafeManagement.Data;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.Report
{
    public class MonthlyReportRepository : BaseRepository<MonthlyReport>, IMonthlyReportRepository
    {
        public MonthlyReportRepository(CafeManagementDbContext _context) : base(_context) { }
        public override async Task<IEnumerable<MonthlyReport>> GetAll()
        {
            return await _context.MonthlyReports.Include(mr=>mr.DailyReports).ToListAsync();
        }
        public async Task<MonthlyReport> GetByMonth(int month, int year)
        {
            (DateOnly start, DateOnly end) = MonthHelper.GetMonthHelper(month, year);
            return await _context.MonthlyReports
                 .Include(mr => mr.DailyReports)
                .FirstOrDefaultAsync(mr => mr.StartDate == start & mr.EndDate == end);
               
        }
    }
}
