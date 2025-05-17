using CafeManagement.Dtos.Request.OrderReq;
using CafeManagement.Dtos.Respone.OrderRes;
using CafeManagement.Enums;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.Order;
using CafeManagement.Models.PromotionModel;

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
                Id = Guid.NewGuid(),
                No = request.No,
                Price = 0,
                Quantity = 0,
                createdAt = DateTime.UtcNow,
                CustomerId = request.CustomerId,
                Note = request.Note,
                OrderStatus = OrderStatus.New

            };
        }

        public NewOrderResponse MapToResponse(Order order)
        {

            return new NewOrderResponse
            {
                Id = order.Id,
                No=order.No,
                Note = order.Note ?? "",
                Total = order.Price,
                Amount = order.Quantity,
                Status = order.OrderStatus,
                CustomerId= order.Customer != null ? order.Customer.Id : null,
                CustomerName = order.Customer != null ? order.Customer.Name : "",
                PromotionId = order.PromotionId,
                CreatedAt=order.createdAt,
                Details =order.Details.Select(od => _orderDetailMapper.MapToResponse(od)).ToList() ?? []
            };
        }
    }
}
