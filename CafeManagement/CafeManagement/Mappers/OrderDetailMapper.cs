using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.Order;

namespace CafeManagement.Mappers
{
    public class OrderDetailMapper : IOrderDetailMapper
    {
        public OrderDetail MapToEntity(OrderDetailRequest request)
        {
            return new OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Note= request.Note,
                Quantity=request.Quantity
            };
        }

        public OrderDetailResponse MapToResponse(OrderDetail orderDetail)
        {
            return new OrderDetailResponse
            {
                Id=orderDetail.Id,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                ProductName = orderDetail.Product.Name,
                Quantity = orderDetail.Quantity,
                Total = orderDetail.Quantity * orderDetail.Product.Price,
                Note = orderDetail.Note ?? ""
            };
        }

        public void UpdateEntityFromRequest(OrderDetail orderDetail, OrderDetailRequest request)
        {
            orderDetail.OrderId = request.OrderId;
            orderDetail.ProductId = request.ProductId;
            orderDetail.Quantity = request.Quantity;
            if (request.Note != null)
                orderDetail.Note = request.Note;
        }
    }
}
