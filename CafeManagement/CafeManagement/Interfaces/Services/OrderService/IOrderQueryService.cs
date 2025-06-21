using CafeManagement.Enums;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Interfaces.Services.OrderService
{
    public interface IOrderQueryService
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<List<Order>> GetByDateAsync(DateOnly date);
        Task<List<Order>> GetByRangeAsync(DateOnly start, DateOnly end);
        Task<List<Order>> GetByStatusAsync(OrderStatus status);
    }
}
