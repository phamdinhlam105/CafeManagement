using CafeManagement.Enums;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services;
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
        public async Task<Order> Update(Order order)
        {
            await _unitOfWork.Order.Update(order);
            return order;
        }

        public async Task<Order> Add(Order order)
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
        public async Task Delete(Order entity)
        {
            await _unitOfWork.Order.Delete(entity);
        }
    }
}
