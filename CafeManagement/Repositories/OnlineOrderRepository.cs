using CafeManagement.Data;
using CafeManagement.Models;
using CafeManagement.Repositories.Interfaces;

namespace CafeManagement.Repositories
{
    public class OnlineOrderRepository:BaseRepository<OnlineOrder>,IOnlineOrderRepository
    {
        public OnlineOrderRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
