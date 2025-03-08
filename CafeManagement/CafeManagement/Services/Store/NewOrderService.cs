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
        private readonly IExportBillService _exportBillService;
        public NewOrderService(IUnitOfWork unitOfWork, IExportBillService exportBillService)
        {
            _unitOfWork = unitOfWork;
            _exportBillService = exportBillService;
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

        public async Task<FinishOrderResponse> FinishOrder(Order order)
        {
            order.OrderStatus = OrderStatus.Completed;
            await _unitOfWork.Order.Update(order);
            return new FinishOrderResponse
            {
                order = order,
                bill = _exportBillService.GenerateInvoicePdf(order)
            };
        }

        public async Task CreateOrder(Order order)
        {
            if (order.Id == Guid.Empty)
                order.Id = Guid.NewGuid();
            await _unitOfWork.Order.Add(order);
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
