using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.OrderModel;
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
                Address = customer.Address,
                NumberOfOrders = customer.NumberOfOrders,
            };
        }

        public Customer MapToEntity(CustomerRequest request)
        {
            return new Customer
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Phone = request.Phone,
                Address = request.Address
            };
        }
        public void UpdateEntityFromRequest(CustomerRequest request, Customer customer)
        {
            customer.Name= request.Name;
            customer.Phone= request.Phone;
            customer.Address= request.Address;
        }
    }
}

