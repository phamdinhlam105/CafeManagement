using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.Stock;

namespace CafeManagement.Events.Registers
{
    public class StockAdjustmentRegister:IEventRegister<StockAdjustment>
    {
        private readonly IObserverFactory<StockAdjustment> _factory;
        public StockAdjustmentRegister(IObserverFactory<StockAdjustment> factory)
        {
            _factory = factory;
        }
        public void Register(ISubject<StockAdjustment> subject)
        {
            subject.Attach(_factory.Create("entry"));
            subject.Attach(_factory.Create("report"));
        }
    }
}
