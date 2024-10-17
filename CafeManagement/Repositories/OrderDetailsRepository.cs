using CafeManagement.Data;
using CafeManagement.Models;
using CafeManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class OrderDetailsRepository:BaseRepository<OrderDetail>,IOrderDetailRepository
    {
        public OrderDetailsRepository(CafeManagementDbContext _context) : base(_context) { }
        public IEnumerable<OrderDetail> GetDetailByOrderId(Guid OrderId)
        {
            return _context.OrderDetails.Where(p => p.OderId == OrderId);
        }
    }
}
