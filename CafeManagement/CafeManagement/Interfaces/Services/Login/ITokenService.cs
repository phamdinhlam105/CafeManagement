using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services.Login
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
