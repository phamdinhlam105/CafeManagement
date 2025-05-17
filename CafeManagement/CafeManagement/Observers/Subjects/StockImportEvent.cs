using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.Order;
using CafeManagement.Models.Stock;

namespace CafeManagement.Observers.Subjects
{
    public class StockImportEvent:ISubject<StockEntry>
    {
        private readonly List<IAppObserver<StockEntry>> _observers = new List<IAppObserver<StockEntry>>();
        public void Attach(IAppObserver<StockEntry> observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IAppObserver<StockEntry> observer)
        {
            _observers.Remove(observer);
        }

        public async Task Notify(StockEntry data)
        {
            foreach (IAppObserver<StockEntry> observer in _observers)
            {
                await observer.Update(data);
            }
        }
    }
}
