using CafeManagement.Enums;
using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Services
{
    public interface INewOrderService
    {
        void CreateOrder(Order order);
        void AddOrderDetail(Order order, OrderDetail detail);
        void ChangeStatus(Order order,OrderStatus status);
        void ChangeNote(Order order, string newnote);
        Order GetById(Guid orderId);
    }
}
