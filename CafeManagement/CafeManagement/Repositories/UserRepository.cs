using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(CafeManagementDbContext _context):base(_context) { }
    }
}
