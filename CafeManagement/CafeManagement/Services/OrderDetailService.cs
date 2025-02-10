using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class OrderDetailService:IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public OrderDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(OrderDetail item)
        {
            _unitOfWork.OrderDetail.Add(item);
        }

        public void Delete(OrderDetail item)
        {
            if (item != null)
                _unitOfWork.OrderDetail.Delete(item);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _unitOfWork.OrderDetail.GetAll();
        }

        public OrderDetail GetById(Guid id) //eagerload already
        {
            return _unitOfWork.OrderDetail.GetById(id);
        }

        public void Update(OrderDetail item)
        {
            if (item != null)
                _unitOfWork.OrderDetail.Update(item);
        }

        public ICollection<OrderDetail> GetDetailsByOrder(Guid orderId) //eager load
        {
            IEnumerable<OrderDetail> allDetails = _unitOfWork.OrderDetail.GetDetailByOrderId(orderId);
            return allDetails.ToList();
        }
    }
}
