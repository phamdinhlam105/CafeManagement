using CafeManagement.Dtos.Respone;
using CafeManagement.Enums;
using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Services
{
    public interface INewOrderService
    {
        Task CreateOrder(Order order);
        Task AddOrderDetail(Order order, OrderDetail detail);
        Task<FinishOrderResponse> FinishOrder(Order order);
        Task EditOrder(Order order);
        Task<Order> GetById(Guid orderId);
        Task<IEnumerable<Order>> GetAll();
    }
}
