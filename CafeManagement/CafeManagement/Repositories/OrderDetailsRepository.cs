using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models.OrderModel;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class OrderDetailsRepository:BaseRepository<OrderDetail>,IOrderDetailRepository
    {
        public OrderDetailsRepository(CafeManagementDbContext _context) : base(_context) { }
        public override async Task<IEnumerable<OrderDetail>> GetAll()
        {
            return await _context.OrderDetails.Include(od => od.Product).ToListAsync();
        }
        public async Task<IEnumerable<OrderDetail>> GetDetailByOrderId(Guid OrderId)
        {
            return await _context.OrderDetails
                .Include(od=>od.Product)
                .Where(p => p.OrderId == OrderId)
                .ToListAsync();
        }
        public override async Task<OrderDetail> GetById(Guid id)
        {
            return await _context.OrderDetails
                .Include(od => od.Product)
                .FirstOrDefaultAsync(od => od.Id == id);
        }
    }
}
