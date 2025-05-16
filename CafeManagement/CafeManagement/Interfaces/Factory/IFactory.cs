namespace CafeManagement.Interfaces.Factory
{
    public interface IFactory<T> where T : class
    {
        T Create(string type);
    }
}
