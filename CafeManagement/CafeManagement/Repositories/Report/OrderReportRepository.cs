using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;

namespace CafeManagement.Repositories.Report
{
    public class OrderReportRepository: BaseRepository<OrderReport>,IOrderReportRepository
    {
        public OrderReportRepository(CafeManagementDbContext _context) : base(_context) { }

    }
}
