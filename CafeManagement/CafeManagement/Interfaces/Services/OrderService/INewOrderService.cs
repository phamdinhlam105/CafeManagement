using CafeManagement.Dtos.Respone;
using CafeManagement.Enums;
using CafeManagement.Models;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Interfaces.Services.OrderService
{
    public interface INewOrderService
    {
        Task<Order> CreateOrder(Order order);
        Task CancelOrder(Guid orderId);
        Task FinishOrder(Order order);
        Task EditOrder(Order order);
        Task<Order> GetById(Guid orderId);
        Task<IEnumerable<Order>> GetAll();
    }
}
