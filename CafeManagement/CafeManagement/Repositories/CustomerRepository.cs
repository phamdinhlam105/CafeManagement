using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class CustomerRepository: BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CafeManagementDbContext _context) : base(_context) {}

        public override async Task Delete(Customer entity)
        {
            if (entity.NumberOfOrders == 0)
                _context.Customers.Remove(entity);
            else
                entity.IsDeleted= true;
            await _context.SaveChangesAsync();
        }
    }
}
