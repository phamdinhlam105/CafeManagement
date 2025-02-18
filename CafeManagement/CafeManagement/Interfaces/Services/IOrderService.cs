using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Services
{
    public interface IOrderService:IService<Order>
    {
        IEnumerable<OrderDetail> GetDetailsByOrderId(Guid orderId);
    }
}
