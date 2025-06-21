using CafeManagement.Events.Obsersvers.FinishOrderObs;
using CafeManagement.Events.Obsersvers.StockUsageLogObs;
using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.Stock;

namespace CafeManagement.Factories.Observers
{
    public class UsageLogObsFactory:IObserverFactory<StockUsageLog>
    {
        private readonly IServiceProvider _serviceProvider;
        public UsageLogObsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IAppObserver<StockUsageLog> Create(string type)
        {
            switch (type)
            {
                case "stock-report": return _serviceProvider.GetRequiredService<StockReportByUsageUpdater>();
                case "stock-entry": return _serviceProvider.GetRequiredService<EntryByUsageUpdater>();
                default:
                    throw new Exception("Invalid type");
            }

        }
    }
}
