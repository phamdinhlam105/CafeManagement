using CafeManagement.Interfaces.Services;
using CafeManagement.Models.Order;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Store
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(OrderDetail item)
        {
            if (item.Id == Guid.Empty)
                item.Id = Guid.NewGuid();
            await _unitOfWork.OrderDetail.Add(item);
        }

        public async Task Delete(OrderDetail item)
        {
            if (item != null)
                await _unitOfWork.OrderDetail.Delete(item);
        }

        public async Task<IEnumerable<OrderDetail>> GetAll()
        {
            return await _unitOfWork.OrderDetail.GetAll();
        }

        public async Task<OrderDetail> GetById(Guid id)
        {
            return await _unitOfWork.OrderDetail.GetById(id);
        }

        public async Task Update(OrderDetail item)
        {
            if (item != null)
                await _unitOfWork.OrderDetail.Update(item);
        }

        public async Task<IEnumerable<OrderDetail>> GetDetailsByOrder(Guid orderId)
        {
            return await _unitOfWork.OrderDetail.GetDetailByOrderId(orderId);
        }
    }
}
