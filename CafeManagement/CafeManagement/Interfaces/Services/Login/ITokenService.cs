using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;

namespace CafeManagement.Interfaces.Services.Login
{
    public interface ITokenService
    {
        LoginResponse? Validate(LoginRequest request);
    }
}
