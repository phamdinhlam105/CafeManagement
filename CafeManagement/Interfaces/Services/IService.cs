using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();

        T GetById(Guid id);

        void Add(T item);

        void Update(T item);

        void Delete(Guid id);
    }
}
