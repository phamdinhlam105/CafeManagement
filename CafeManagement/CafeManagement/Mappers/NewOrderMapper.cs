using CafeManagement.Dtos.Request.OrderReq;
using CafeManagement.Dtos.Respone.OrderRes;
using CafeManagement.Enums;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Mappers
{
    public class NewOrderMapper : INewOrderMapper
    {
        private readonly IOrderDetailMapper _orderDetailMapper;
        public NewOrderMapper(IOrderDetailMapper orderDetailMapper)
        {
            _orderDetailMapper = orderDetailMapper;
        }
        public Order MapToEntity(NewOrderRequest request)
        {
            return new Order
            {
                TableNo = request.TableNo,
                Price = 0,
                Quantity = 0,
                CustomerId = request.CustomerId,
                Note = request.Note,
                OrderStatus = OrderStatus.New

            };
        }

        public NewOrderResponse MapToResponse(Order order)
        {

            return new NewOrderResponse
            {
                Id=order.Id,
                TableNo = order.TableNo,
                Note = order.Note ?? "",
                Total = order.Price,
                Amount = order.Quantity,
                Status = order.OrderStatus,
                CustomerId = order.Customer.Id,
                CustomerName = order.Customer.Name,
                PromotionId = order.PromotionId,
                CreatedAt=order.CreatedAt,
                CreatedByUserId= order.CreateByUserId,
                CreatedByUserName=order.CreateByUser.Profile.Name,
                LastUpdateAt=order.LastUpdateAt,
                LastUpdateByUserId=order.LastUpdateByUserId, 
                LastUpdateByUserName=order.LastUpdateByUser.Profile.Name,
                Details = order.Details.Select(od => _orderDetailMapper.MapToResponse(od)).ToList() ?? []
            };
        }
    }
}
