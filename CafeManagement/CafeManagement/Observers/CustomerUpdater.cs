using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models.Order;

namespace CafeManagement.Observers
{
    public class CustomerUpdater : IAppObserver<Order>
    {
        private readonly ICustomerService _customerService;
        public CustomerUpdater(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task Update(Order data)
        {
            data.Customer.NumberOfOrders++;
                await _customerService.Update(data.Customer);
        }
    }
}
