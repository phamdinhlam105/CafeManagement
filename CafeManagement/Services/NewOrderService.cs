using CafeManagement.Enums;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class NewOrderService : INewOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddOrderDetail(Order order, OrderDetail detail)
        {
            order.Details.Add(detail);
            _unitOfWork.OrderDetail.Add(detail);
        }

        public void ChangeStatus(Order order, OrderStatus status)
        {
            order.OrderStatus = status;
            _unitOfWork.Order.Update(order);
        }

        public void CreateOrder(Order order)
        {
            _unitOfWork.Order.Add(order);
            if (order is OnlineOrder onlineOrder)
                _unitOfWork.OnlineOrder.Add(onlineOrder);
        }

        public Order GetById(Guid orderId)
        {
            return _unitOfWork.Order.GetById(orderId);
        }
    }
}
