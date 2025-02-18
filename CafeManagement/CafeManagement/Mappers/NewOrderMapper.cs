using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Enums;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.Order;

namespace CafeManagement.Mappers
{
    public class NewOrderMapper : INewOrderMapper
    {
        public Order MapToEntity(NewOrderRequest request)
        {
            if (request.OrderType == OrderType.InStore)
                return new InStoreOrder
                {
                    Id = new Guid(),
                    Note = request.Note,
                    OrderStatus=OrderStatus.New,
                    CustomerId=request.CustomerId

                };
            else
                return new OnlineOrder
                {
                    Id = new Guid(),
                    Note = request.Note,
                    OrderStatus = OrderStatus.New,
                    CustomerId = request.CustomerId
                };
        }

        public NewOrderResponse MapToResponse(Order order)
        {

            NewOrderResponse response = new NewOrderResponse
            {
                Id = order.Id,
                Note = order.Note,
                Total = order.Price,
                Amount = order.Quantity,
                OrderType = order.OrderType,
                Status = order.OrderStatus
            };
            if (order is OnlineOrder onlineOrder)
            {
                response.ShippingCost = onlineOrder.ShippingCost;
                response.DeliveryTime = onlineOrder.DeliveryTime;
            }
            return response;
        }

        public void UpdateEntityFromRequest(Order order, NewOrderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
