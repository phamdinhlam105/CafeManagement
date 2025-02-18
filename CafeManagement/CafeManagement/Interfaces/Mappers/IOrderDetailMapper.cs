using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IOrderDetailMapper
    {
        OrderDetailResponse MapToResponse(OrderDetail orderDetail);
        OrderDetail MapToEntity(OrderDetailRequest request);
        void UpdateEntityFromRequest(OrderDetail orderDetail, OrderDetailRequest request);
    }
}
