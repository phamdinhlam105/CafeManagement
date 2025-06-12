using CafeManagement.Models;
using System.Threading.Tasks;

namespace CafeManagement.Interfaces.Services
{
    public interface IAdd<T>
    {
        Task<T> Add(T entity);
    }

    public interface IUpdate<T>
    {
        Task<T> Update(T entity);
    }

    public interface IDelete<T>
    {
        Task Delete(T entity);
    }

    public interface IGetById<T>
    {
        Task<T> GetById(Guid id);
    }

    public interface IGetAll<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
    public interface IService<T>:IAdd<T>,IUpdate<T>,IDelete<T>,IGetById<T>,IGetAll<T>
    {
       
    }
}
