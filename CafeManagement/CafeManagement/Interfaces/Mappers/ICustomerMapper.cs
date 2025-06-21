using CafeManagement.Dtos.Request.OrderReq;
using CafeManagement.Dtos.Response.OrderRes;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Interfaces.Mappers
{
    public interface ICustomerMapper: IRequestToEntity<CustomerRequest, Customer>,
        IRequestToUpdate<CustomerRequest, Customer>,
        IEntityToResponse<Customer, CustomerResponse>
    {
    }
}
