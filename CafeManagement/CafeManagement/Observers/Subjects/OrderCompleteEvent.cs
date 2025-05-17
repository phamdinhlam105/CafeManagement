
using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.Order;

namespace CafeManagement.Observers.Subjects
{
    public class OrderCompleteEvent : ISubject<Order>
    {
        private readonly List<IAppObserver<Order>> _observers = new List<IAppObserver<Order>>();
        public void Attach(IAppObserver<Order> observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IAppObserver<Order> observer)
        {
            _observers.Remove(observer);
        }

        public async Task Notify(Order data)
        {
            foreach (IAppObserver<Order> observer in _observers)
            {
                await observer.Update(data);
            }
        }
    }
}
