namespace CafeManagement.Interfaces.Observer
{
    public interface ISubject<T> where T : class
    {
        void Attach(IAppObserver<T> observer);
        void Detach(IAppObserver<T> observer);
        Task Notify(T data);
    }
}
