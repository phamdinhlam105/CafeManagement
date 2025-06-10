using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.Report
{
    public class DailyReportRepository:BaseRepository<DailyReport>,IDailyReportRepository
    {
        public DailyReportRepository(CafeManagementDbContext _context) : base(_context) { }

        public async Task<List<DailyReport>> GetByDateRange(DateOnly start, DateOnly end)
        {
            return await _context.DailyReports
                .Include(dr => dr.OrderReport)
                .Include(dr => dr.StockReport)
                .Include(dr=>dr.ProductReports)
                .Where(dr => dr.ReportDate >=start && dr.ReportDate<=end)
                .ToListAsync();
        }
        public override async Task<DailyReport> GetById(Guid id)
        {
            return await _context.DailyReports
                .Include(dr => dr.OrderReport)
                .Include(dr => dr.StockReport)
                .Include(dr => dr.ProductReports)
                .FirstOrDefaultAsync(dr=>dr.Id==id);
        }
        public async Task<DailyReport> GetByDate(DateOnly date)
        {
            return await _context.DailyReports
               .Include(dr => dr.OrderReport)
               .Include(dr => dr.StockReport)
               .Include(dr => dr.ProductReports)
               .FirstOrDefaultAsync(dr => dr.ReportDate == date);
        }
        public async Task<List<DailyReport>> GetAll()
        {
            return await _context.DailyReports
              .Include(dr => dr.OrderReport)
              .Include(dr => dr.StockReport)
              .Include(dr => dr.ProductReports)
              .ToListAsync();
        }
        public async Task<DailyReport> GetLastestReport()
        {
            var latestDate = await _context.DailyReports
               .MaxAsync(ds => ds.ReportDate);
            return await _context.DailyReports
               .Include(dr => dr.OrderReport)
               .Include(dr => dr.StockReport)
               .Include(dr => dr.ProductReports)
               .FirstOrDefaultAsync(ds => ds.ReportDate == latestDate);
        }
    }
}
