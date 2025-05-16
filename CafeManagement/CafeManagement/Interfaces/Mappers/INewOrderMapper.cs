using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Mappers
{
    public interface INewOrderMapper: IRequestToEntity<NewOrderRequest, Order>,
        IEntityToResponse<Order, NewOrderResponse>
    {
       
    }
}
