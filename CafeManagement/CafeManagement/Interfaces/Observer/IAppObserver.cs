namespace CafeManagement.Interfaces.Observer
{
    public interface IAppObserver<T> where T : class
    {
        Task Update(T data);
    }
}
