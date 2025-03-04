using CafeManagement.Models;

namespace CafeManagement.Interfaces.Repositories
{
    public interface IProfileRepository:IRepository<Profile>
    {
        Task<Profile> GetByUserId(string userId);
    }
}
