using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;

namespace CafeManagement.Repositories.Stock
{
    public class AdjustmentDetailRepository:BaseRepository<AdjustmentDetail>,IAdjustmentDetailRepository
    {
        public AdjustmentDetailRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
