using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.Stock;

namespace CafeManagement.Events.Registers
{
    public class NewUsageLogRegister : IEventRegister<StockUsageLog>
    {
        private readonly IObserverFactory<StockUsageLog> _stockUsageObsFactory;
        public NewUsageLogRegister(IObserverFactory<StockUsageLog> stockUsageObsFactory)
        {
            _stockUsageObsFactory = stockUsageObsFactory;
        }

        public void Register(ISubject<StockUsageLog> subject)
        {
            subject.Attach(_stockUsageObsFactory.Create("stock-report"));
            subject.Attach(_stockUsageObsFactory.Create("stock-entry"));
        }
    }
}
