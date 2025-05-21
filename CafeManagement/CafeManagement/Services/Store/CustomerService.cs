using CafeManagement.Interfaces.Services;
using CafeManagement.Models.Order;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Store
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> Add(Customer item)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }
            await _unitOfWork.Customer.Add(item);
            return item;
        }

        public async Task Delete(Customer item)
        {
            if (item != null)
                await _unitOfWork.Customer.Delete(item);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _unitOfWork.Customer.GetAll();
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _unitOfWork.Customer.GetById(id);
        }

        public async Task<Customer> Update(Customer item)
        {
            if (item != null)
                await _unitOfWork.Customer.Update(item);
            return item;
        }
    }
}
