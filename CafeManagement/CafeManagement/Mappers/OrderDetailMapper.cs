using CafeManagement.Dtos.Request.OrderReq;
using CafeManagement.Dtos.Respone.OrderRes;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.OrderModel;

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
                Quantity=request.Quantity,
                CurrentPrice= request.CurrentPrice,
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
                Price=orderDetail.CurrentPrice,
                Total = orderDetail.Quantity * orderDetail.CurrentPrice,
                Note = orderDetail.Note ?? ""
            };
        }

        public void UpdateEntityFromRequest(OrderDetailRequest request, OrderDetail orderDetail)
        {
            orderDetail.OrderId = request.OrderId;
            orderDetail.ProductId = request.ProductId;
            orderDetail.Quantity = request.Quantity;
            if (request.Note != null)
                orderDetail.Note = request.Note;
        }
    }
}
