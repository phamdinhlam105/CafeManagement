using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class OrderDetailsRepository:BaseRepository<OrderDetail>,IOrderDetailRepository
    {
        public OrderDetailsRepository(CafeManagementDbContext _context) : base(_context) { }
        public IEnumerable<OrderDetail> GetDetailByOrderId(Guid OrderId)
        {
            return _context.OrderDetails
                .Where(p => p.OderId == OrderId)
                .Include(od => od.Order)
                .Include(od => od.Product);
        }
        public override OrderDetail GetById(Guid id)
        {
            return _context.OrderDetails
                .Include(od => od.Product)
                .FirstOrDefault(od => od.Id == id);
        }
    }
}
