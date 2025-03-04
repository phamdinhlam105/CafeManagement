using CafeManagement.Interfaces.Services;
using CafeManagement.Models.Order;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class OrderService:IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(Order item)
        {
            await _unitOfWork.Order.Add(item);
        }

        public async Task Delete(Order item)
        {
            if (item != null)
                await _unitOfWork.Order.Delete(item);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _unitOfWork.Order.GetAll();
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _unitOfWork.Order.GetById(id);
        }

        public async Task Update(Order item)
        {
            if (item != null)
                await _unitOfWork.Order.Update(item);
        }
        public async Task<IEnumerable<OrderDetail>> GetDetailsByOrderId(Guid orderId)
        {
            return await _unitOfWork.OrderDetail.GetDetailByOrderId(orderId);
        }
    }
}
