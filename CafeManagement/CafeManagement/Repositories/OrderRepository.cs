using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class OrderRepository: BaseRepository<Order>,IOrderRepository
    {
        public OrderRepository(CafeManagementDbContext _context):base(_context) { }

        public override Order GetById(Guid id)
        {
            return _context.Orders
                .Include(o=>o.Details)
                .FirstOrDefault(o => o.Id == id);
        }
    }
}
