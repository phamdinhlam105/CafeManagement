using CafeManagement.Enums;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Interfaces.Services.OrderService
{
    public interface IOrderStatusService
    {
        Task FinishOrder(Order order);
        Task CancelOrder(Order order);
    }
}
