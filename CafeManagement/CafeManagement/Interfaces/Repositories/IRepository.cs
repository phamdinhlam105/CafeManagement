namespace CafeManagement.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task Add(T item);
        Task Update(T item);
        Task Delete(T item);
    }
}
