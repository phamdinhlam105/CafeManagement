using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;
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
