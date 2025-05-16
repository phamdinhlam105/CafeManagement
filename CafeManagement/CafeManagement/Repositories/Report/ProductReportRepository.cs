
using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Models.Report;

namespace CafeManagement.Repositories.Report
{
    public class ProductReportRepository:BaseRepository<ProductReport>,IProductReportRepository
    {
        public ProductReportRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
