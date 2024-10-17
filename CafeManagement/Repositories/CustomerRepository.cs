using CafeManagement.Data;
using CafeManagement.Models;
using CafeManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class CustomerRepository: BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CafeManagementDbContext _context) : base(_context)
        {

        }
    }
}
