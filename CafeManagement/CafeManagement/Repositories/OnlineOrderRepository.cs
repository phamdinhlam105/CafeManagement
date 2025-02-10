using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;

namespace CafeManagement.Repositories
{
    public class OnlineOrderRepository:BaseRepository<OnlineOrder>,IOnlineOrderRepository
    {
        public OnlineOrderRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
