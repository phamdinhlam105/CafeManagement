using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.Stock;
using System;

namespace CafeManagement.Observers.Subjects
{
    public class BaseSubjectEvent<T>: ISubject<T>
         where T : class
    {
        private readonly List<IAppObserver<T>> _observers = new List<IAppObserver<T>>();
        public void Attach(IAppObserver<T> observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IAppObserver<T> observer)
        {
            _observers.Remove(observer);
        }

        public async Task Notify(T data)
        {
            foreach (IAppObserver<T> observer in _observers)
            {
                await observer.Update(data);
            }
        }
    }
}
