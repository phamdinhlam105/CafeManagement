using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.Stock;

namespace CafeManagement.Events.Registers
{
    public class StockImportRegister : IEventRegister<StockEntry>
    {
        private readonly IAppObserver<StockEntry> _observer;
        public StockImportRegister(IAppObserver<StockEntry> observer)
        {
            _observer = observer;
        }
        public void Register(ISubject<StockEntry> subject)
        {
            subject.Attach(_observer);
        }
    }
}
