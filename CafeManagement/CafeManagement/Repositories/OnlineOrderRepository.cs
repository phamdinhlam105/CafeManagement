using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models.Order;

namespace CafeManagement.Repositories
{
    public class OnlineOrderRepository:BaseRepository<OnlineOrder>,IOnlineOrderRepository
    {
        public OnlineOrderRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
