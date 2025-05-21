using CafeManagement.Interfaces.Observer;

namespace CafeManagement.Interfaces.Factory
{
    public interface IObserverFactory<T> where T : class
    {
        IAppObserver<T> Create(string type);
    }
}
