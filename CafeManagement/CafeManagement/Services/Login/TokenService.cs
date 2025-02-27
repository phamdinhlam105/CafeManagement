using Azure.Core;
using CafeManagement.Data;
using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Services.Login;
using CafeManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CafeManagement.Services.Login
{
    public class TokenService:ITokenService
    {
        private CafeManagementDbContext _dbcontext;
        private IConfiguration _configuration;
        public TokenService(CafeManagementDbContext dbcontext, IConfiguration configuration)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
        }

        private string CreateToken(Profile user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
           
            var secetkeyBytes = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]);
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                        new Claim("Name", user.Name),
                        new Claim("Age", user.Age.ToString()),
                        new Claim(ClaimValueTypes.Email, user.Email),
                        new Claim("PhoneNumber", user.PhoneNumber),
                        new Claim("PictureURL", user.PictureURL),
                        new Claim("JoinDate", user.joinDate.ToString()),
                        //new Claim("Role",user.Role.RoleName.ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddMinutes(180),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secetkeyBytes), SecurityAlgorithms.HmacSha512Signature)

            };
            var token = jwtTokenHandler.CreateToken(tokenDesciptor);
            return jwtTokenHandler.WriteToken(token);
        }

        public LoginResponse? Validate(LoginRequest request)
        {

            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return null;
            var userAccount = _dbcontext.Users
                .Include(u => u.Profile)
                .FirstOrDefault(u => u.UserName == request.UserName);
            if (userAccount == null)
                return null;

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(userAccount, userAccount.PasswordHash, request.Password);

            if (result != PasswordVerificationResult.Success)
                return null;

            if (userAccount.Profile == null)
            {
                userAccount.Profile = new Profile
                {
                    Name = userAccount.UserName,
                    Email = userAccount.Email, 
                    PhoneNumber = "111111",
                    joinDate = DateTime.UtcNow,
                    Age = 0,
                    PictureURL = ""
                };
                _dbcontext.SaveChanges();
            }
            var token = CreateToken(userAccount.Profile);
            return new LoginResponse
            {
                UserName = request.UserName,
                ExpiresIn = 180,
                AccessToken = token 
            };
        }
    }
}
