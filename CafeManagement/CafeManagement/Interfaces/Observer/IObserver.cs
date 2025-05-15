namespace CafeManagement.Interfaces.Observer
{
    public interface IObserver
    {
        Task Update(object data);
    }
}
