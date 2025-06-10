using CafeManagement.Dtos.Request.UserReq;
using CafeManagement.Dtos.Respone.UserRes;
using CafeManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace CafeManagement.Interfaces.Services.Login
{
    public interface IUserService
    {
        Task<RegisterResponse> Signup(SignupRequest request);
        Task Update(User user);
        Task<LoginResponse> Login(LoginRequest request);
        Task ChangeRole(SetRoleRequest request);
        Task ChangePassword(ChangePasswordRequest request, string userId);
        Task<Profile> UpdateProfile(Profile profile, string userId);
        Task<Profile> GetProfileById(string id);
        Task<IEnumerable<UserResponse>> GetAllUser();
        Task<List<IdentityRole>> GetAllRole();
    }
}
