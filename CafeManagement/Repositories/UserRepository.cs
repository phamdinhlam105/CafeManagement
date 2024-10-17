using CafeManagement.Data;
using CafeManagement.Models;
using CafeManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(CafeManagementDbContext _context):base(_context) { }
    }
}
