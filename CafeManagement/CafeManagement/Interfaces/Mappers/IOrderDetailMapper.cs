using CafeManagement.Dtos.Request.OrderReq;
using CafeManagement.Dtos.Respone.OrderRes;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IOrderDetailMapper : IRequestToEntity<OrderDetailRequest, OrderDetail>,
        IEntityToResponse<OrderDetail, OrderDetailResponse>,
        IRequestToUpdate<OrderDetailRequest, OrderDetail>
    {

    }
}
