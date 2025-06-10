namespace CafeManagement.Interfaces.Observer
{
    public interface IEventRegister<T> where T : class
    {
        void Register(ISubject<T> subject);
    }
}
