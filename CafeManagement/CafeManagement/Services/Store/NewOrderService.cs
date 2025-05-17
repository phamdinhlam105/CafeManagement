using CafeManagement.Enums;
using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.Models.Order;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Store
{
    public class NewOrderService : INewOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubject _orderCompleteEvent;
        private readonly IObserverFactory<IAppObserver> _factory;
        public NewOrderService(IUnitOfWork unitOfWork, ISubject orderCompleteEvent, IObserverFactory<IAppObserver> factory)
        {
            _unitOfWork = unitOfWork;
            _orderCompleteEvent = orderCompleteEvent;
            _factory = factory;
            _orderCompleteEvent.Attach(_factory.Create("customer"));
            _orderCompleteEvent.Attach(_factory.Create("report"));
        }
        public async Task AddOrderDetail(Order order, OrderDetail detail,Product product)
        {
            if (detail.Id == Guid.Empty)
                detail.Id = Guid.NewGuid();
            order.Quantity += detail.Quantity;
            order.Price += product.Price * detail.Quantity;
            await _unitOfWork.Order.Update(order);
            await _unitOfWork.OrderDetail.Add(detail);
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
