using CafeManagement.Interfaces.Observer;

namespace CafeManagement.Services.ObserverPattern
{
    public class OrderCompleteEvent : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public async Task Notify(object data)
        {
            foreach (IObserver observer in _observers)
            {
                await observer.Update(data);
            }
        }
    }
}
