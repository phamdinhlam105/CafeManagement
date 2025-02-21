using CafeManagement.Data;
using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CafeManagement.Services.Login
{
    public class JwtService
    {
        private CafeManagementDbContext _dbcontext;
        private IConfiguration _configuration;
        public JwtService(CafeManagementDbContext dbcontext, IConfiguration configuration)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
        }

        public LoginResponse? Authentication(LoginRequest request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return null;
            var userAccount = _dbcontext.Users.SingleOrDefault(u => u.UserName == request.UserName && u.Password == request.Password);
            if (userAccount == null)
                return null;
            var secetkeyBytes = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]);
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserName", userAccount.UserName),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(180),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secetkeyBytes), SecurityAlgorithms.HmacSha512Signature)

            };
            var token = jwtTokenHandler.CreateToken(tokenDesciptor);
            return new LoginResponse
            {
                UserName = userAccount.UserName,
                ExpiresIn = 180,
                AccessToken = jwtTokenHandler.WriteToken(token)
            };
        }
    }
}
