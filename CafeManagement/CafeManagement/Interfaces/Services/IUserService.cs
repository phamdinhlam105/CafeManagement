using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services
{
    public interface IUserService
    {
        Task Signup(SignupRequest request);
        Task Update(User user);
        Task<LoginResponse> Login(LoginRequest request);
        Task ChangeRole(SetRoleRequest request);
    }
}
