using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public void Add(Customer item)
        {
            _unitOfWork.Customer.Add(item);
        }

        public void Delete(Guid id)
        {
            var item = _unitOfWork.Customer.GetById(id);
            if (item != null)
                _unitOfWork.Customer.Delete(item);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _unitOfWork.Customer.GetAll();
        }

        public Customer GetById(Guid id)
        {
            return _unitOfWork.Customer.GetById(id);
        }

        public void Update(Customer item)
        {
            if (item != null)
                _unitOfWork.Customer.Update(item);
        }
    }
}
