using CafeManagement.Dtos.Request.OrderReq;
using CafeManagement.Dtos.Respone.OrderRes;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Interfaces.Mappers
{
    public interface INewOrderMapper: IRequestToEntity<NewOrderRequest, Order>,
        IEntityToResponse<Order, NewOrderResponse>
    {
       
    }
}
