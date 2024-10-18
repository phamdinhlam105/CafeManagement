using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class OrderRepository: BaseRepository<Order>,IOrderRepository
    {
        public OrderRepository(CafeManagementDbContext _context):base(_context) { }
    }
}
