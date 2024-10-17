using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class OrderService:IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public void Add(Order item)
        {
            _unitOfWork.Order.Add(item);
        }

        public void Delete(Guid id)
        {
            var item = _unitOfWork.Order.GetById(id);
            if (item != null)
                _unitOfWork.Order.Delete(item);
        }

        public IEnumerable<Order> GetAll()
        {
            return _unitOfWork.Order.GetAll();
        }

        public Order GetById(Guid id)
        {
            return _unitOfWork.Order.GetById(id);
        }

        public void Update(Order item)
        {
            if (item != null)
                _unitOfWork.Order.Update(item);
        }
        public IEnumerable<OrderDetail> GetDetailsByOrderId(Guid orderId)
        {
            return _unitOfWork.OrderDetail.GetAll().Where(q => q.OderId == orderId).ToList();
        }
    }
}
