using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.OrderService;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Events.Obsersvers.FinishOrderObs
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
