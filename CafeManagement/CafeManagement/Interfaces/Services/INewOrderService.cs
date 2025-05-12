using CafeManagement.Dtos.Respone;
using CafeManagement.Enums;
using CafeManagement.Models;
using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Services
{
    public interface INewOrderService
    {
        Task<Order> CreateOrder(Order order);
        Task CancelOrder(Guid orderId);
        Task AddOrderDetail(Order order, OrderDetail detail,Product product);
        Task FinishOrder(Order order);
        Task EditOrder(Order order);
        Task<Order> GetById(Guid orderId);
        Task<IEnumerable<Order>> GetAll();
    }
}
