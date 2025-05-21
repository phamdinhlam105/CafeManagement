using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.Order;
using CafeManagement.Models.Stock;
using CafeManagement.Observers;

namespace CafeManagement.Factories.Observers
{
    public class StockObsFactory:IObserverFactory<StockEntry>
    {
        private readonly IServiceProvider _serviceProvider;
        public StockObsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IAppObserver<StockEntry> Create(string type)
        {
            switch (type)
            {
                case "stock": return _serviceProvider.GetRequiredService<StockUpdater>();
                case "report": return _serviceProvider.GetRequiredService<StockReportUpdater>();
                default:
                    throw new Exception("Invalid type");
            }
        }
    }
}
