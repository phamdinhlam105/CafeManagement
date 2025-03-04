using CafeManagement.Models;
using System.Threading.Tasks;

namespace CafeManagement.Interfaces.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);

        Task Add(T item);

        Task Update(T item);

        Task Delete(T item);
    }
}
