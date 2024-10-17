using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models;

namespace CafeManagement.Mappers
{
    public class CustomerMapper:ICustomerMapper
    {
        public CustomerResponse MapToResponse(Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public Customer MapToEntity(CustomerRequest request)
        {
            return new Customer
            {
                Id = new Guid(),
                Name = request.Name,
                Phone = request.Phone,
                Address = request.Address
            };
        }
    }
}

