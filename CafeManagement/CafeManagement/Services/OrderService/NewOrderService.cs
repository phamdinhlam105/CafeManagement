using CafeManagement.Enums;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.OrderService;
using CafeManagement.Models.OrderModel;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.OrderService
{
    public class NewOrderService : INewOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubject<Order> _orderCompleteEvent;
        private readonly IEventRegister<Order> _orderCompleteRegister;
        public NewOrderService(IUnitOfWork unitOfWork,
            ISubject<Order> orderCompleteEvent,
            IEventRegister<Order> orderCompleteRegister)
        {
            _unitOfWork = unitOfWork;
            _orderCompleteEvent = orderCompleteEvent;
            _orderCompleteRegister = orderCompleteRegister;
            _orderCompleteRegister.Register(_orderCompleteEvent);
        }
        public async Task EditOrder(Order order)
        {
            await _unitOfWork.Order.Update(order);
        }

        public async Task FinishOrder(Order order)
        {
            order.OrderStatus = OrderStatus.Completed;
            await _unitOfWork.Order.Update(order);
            await _orderCompleteEvent.Notify(order);
        }

        public async Task CancelOrder(Guid orderId)
        {
            var currentOrder = await _unitOfWork.Order.GetById(orderId) ?? throw new Exception("id not found");
            currentOrder.OrderStatus = OrderStatus.Cancelled;
            await _unitOfWork.Order.Update(currentOrder);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            if (order.Id == Guid.Empty)
                order.Id = Guid.NewGuid();
            await _unitOfWork.Order.Add(order);
            var newOrder = await _unitOfWork.Order.GetById(order.Id);
            return newOrder;
        }

        public async Task<Order> GetById(Guid orderId)
        {
            Order order = await _unitOfWork.Order.GetById(orderId);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _unitOfWork.Order.GetAll();
        }
    }
}
