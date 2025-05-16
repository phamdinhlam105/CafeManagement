using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;

namespace CafeManagement.Repositories.Report
{
    public class StockReportRepository:BaseRepository<StockReport>,IStockReportRepository
    {
        public StockReportRepository(CafeManagementDbContext _context):base(_context) { }
    }
}
