using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Observers;

namespace CafeManagement.Factories
{
    public class ObserverFactory : IFactory<IObserver>
    {
        private readonly IServiceProvider _serviceProvider;
        public ObserverFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IObserver Create(string type)
        {
            switch(type)
            {
                case "customer": return _serviceProvider.GetRequiredService<CustomerUpdater>();
                case "report": return _serviceProvider.GetRequiredService<ReportUpdater>();
                default:
                    throw new Exception("Invalid type");
            }
            
        }
    }
}
