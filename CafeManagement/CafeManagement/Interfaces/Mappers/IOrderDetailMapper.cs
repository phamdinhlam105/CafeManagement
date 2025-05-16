using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IOrderDetailMapper : IRequestToEntity<OrderDetailRequest, OrderDetail>,
        IEntityToResponse<OrderDetail, OrderDetailResponse>,
        IRequestToUpdate<OrderDetailRequest, OrderDetail>
    {

    }
}
