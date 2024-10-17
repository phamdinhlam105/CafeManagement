using CafeManagement.Data;
using CafeManagement.Models;
using CafeManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class OrderRepository: BaseRepository<Order>,IOrderRepository
    {
        public OrderRepository(CafeManagementDbContext _context):base(_context) { }
    }
}
