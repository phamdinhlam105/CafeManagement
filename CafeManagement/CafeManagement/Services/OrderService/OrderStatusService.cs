using CafeManagement.Enums;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.OrderService;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Services.OrderService
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly ISubject<Order> _finishOrderEvent;
        private readonly INewOrderService _newOrderService;
        private readonly IEventRegister<Order> _finishOrderRegiser;
        public OrderStatusService(ISubject<Order> finishOrderEvent, 
            INewOrderService newOrderService,
            IEventRegister<Order> finishOrderRegister)
        {
            _finishOrderEvent = finishOrderEvent;
            _newOrderService = newOrderService;
            _finishOrderRegiser = finishOrderRegister;
        }

        public async Task CancelOrder(Order order)
        {
            
        }

        public async Task FinishOrder(Order order)
        {
            await _newOrderService.Update(order);
            _finishOrderRegiser.Register(_finishOrderEvent);
            await _finishOrderEvent.Notify(order);
            
        }
    }
}
