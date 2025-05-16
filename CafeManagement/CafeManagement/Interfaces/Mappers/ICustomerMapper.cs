using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models;

namespace CafeManagement.Interfaces.Mappers
{
    public interface ICustomerMapper: IRequestToEntity<CustomerRequest, Customer>,
        IRequestToUpdate<CustomerRequest, Customer>,
        IEntityToResponse<Customer, CustomerResponse>
    {
    }
}
