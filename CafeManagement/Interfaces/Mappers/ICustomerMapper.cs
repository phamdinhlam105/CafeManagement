using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Models;

namespace CafeManagement.Interfaces.Mappers
{
    public interface ICustomerMapper
    {
        CustomerResponse MapToResponse(Customer customer);
        Customer MapToEntity(CustomerRequest request);
    }
}
