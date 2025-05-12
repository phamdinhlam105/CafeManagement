using CafeManagement.Dtos.Respone;
using CafeManagement.Enums;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.Models.Order;
using CafeManagement.UnitOfWork;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System.Text;

namespace CafeManagement.Services.Store
{
    public class NewOrderService : INewOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewOrderService(IUnitOfWork unitOfWork, IExportBillService exportBillService)
        {
            _unitOfWork = unitOfWork;
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
            order.Customer.NumberOfOrders++;
            await _unitOfWork.Order.Update(order);
            await _unitOfWork.Customer.Update(order.Customer);
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
