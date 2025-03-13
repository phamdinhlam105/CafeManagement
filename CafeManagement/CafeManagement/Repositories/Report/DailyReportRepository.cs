using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CafeManagement.Repositories.Report
{
    public class DailyReportRepository:BaseRepository<DailyReport>,IDailyReportRepository
    {
        public DailyReportRepository(CafeManagementDbContext _context) : base(_context) { }

        public async Task<IEnumerable<DailyReport>> GetByDateRange(DateOnly start, DateOnly end)
        {
            return await _context.DailyReports
                .Include(dr => dr.TopSelling)
                .Include(dr => dr.LeastSelling)
                .Where(dr => dr.ReportDate >=start && dr.ReportDate<=end)
                .ToListAsync();
        }
        public override async Task<DailyReport> GetById(Guid id)
        {
            return await _context.DailyReports
                .Include(dr => dr.TopSelling)
                .Include(dr => dr.LeastSelling)
                .FirstOrDefaultAsync(dr=>dr.Id==id);
        }
        public async Task<DailyReport> GetByDate(DateOnly date)
        {
            return await _context.DailyReports
               .Include(dr => dr.TopSelling)
               .Include(dr => dr.LeastSelling)
               .FirstOrDefaultAsync(dr => dr.ReportDate == date);
        }
        public async Task<IEnumerable<DailyReport>> GetAll()
        {
            return await _context.DailyReports
              .Include(dr => dr.TopSelling)
              .Include(dr => dr.LeastSelling)
              .ToListAsync();
        }
    }
}
