using CafeManagement.Events.Obsersvers.StockAdjustmentObs;
using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.Stock;

namespace CafeManagement.Factories.Observers
{
    public class AdjustmentObsFactory:IObserverFactory<StockAdjustment>
    {
        private readonly IServiceProvider _serviceProvider;
        public AdjustmentObsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IAppObserver<StockAdjustment> Create(string type)
        {
            switch (type)
            {
                case "entry": return _serviceProvider.GetRequiredService<EntryByAdjustmentUpdater>();
                case "report": return _serviceProvider.GetRequiredService<StockReportByAdjustmentUpdater>();
                default:
                    throw new Exception("Invalid type");
            }
        }
    }
}
