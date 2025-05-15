namespace CafeManagement.Interfaces.Observer
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        Task Notify(object data);
    }
}
