using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.Order;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Observers
{
    public class CustomerUpdater : IObserver
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerUpdater(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Update(object data)
        {
            if (data is Order order)
            {
                order.Customer.NumberOfOrders++;
                await _unitOfWork.Customer.Update(order.Customer);
            }
            else
                throw new Exception("Not valid data type");
        }
    }
}
