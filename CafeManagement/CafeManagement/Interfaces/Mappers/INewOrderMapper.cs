using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Models;

namespace CafeManagement.Interfaces.Mappers
{
    public interface INewOrderMapper
    {
        NewOrderResponse MapToResponse(Order order);
        Order MapToEntity(NewOrderRequest request);
        void UpdateEntityFromRequest(Order order, NewOrderRequest request);
    }
}
