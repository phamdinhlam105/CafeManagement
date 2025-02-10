namespace CafeManagement.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid Id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
