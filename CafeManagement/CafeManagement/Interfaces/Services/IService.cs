using CafeManagement.Models;
using System.Threading.Tasks;

namespace CafeManagement.Interfaces.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);

        Task<T> Add(T item);

        Task<T> Update(T item);

        Task Delete(T item);
    }
}
