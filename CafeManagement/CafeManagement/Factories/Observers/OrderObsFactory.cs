using CafeManagement.Events.Obsersvers;
using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Factories.Observers
{
    public class OrderObsFactory : IObserverFactory<Order>
    {
        private readonly IServiceProvider _serviceProvider;
        public OrderObsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IAppObserver<Order> Create(string type)
        {
            switch (type)
            {
                case "customer": return _serviceProvider.GetRequiredService<CustomerUpdater>();
                case "report": return _serviceProvider.GetRequiredService<OrderReportUpdater>();
                default:
                    throw new Exception("Invalid type");
            }

        }
    }
}
