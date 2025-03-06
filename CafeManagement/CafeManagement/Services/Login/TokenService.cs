﻿using Azure.Core;
using CafeManagement.Data;
using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Enums;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Login;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CafeManagement.Services.Login
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public TokenService(IConfiguration configuration,UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> CreateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            
            var secetkeyBytes = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]);
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            string roleName = userRole != null && Enum.TryParse(typeof(UserRole), userRole, out var roleEnum)
            ? Role.GetRoleName((UserRole)roleEnum)
    : string.Empty;
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                    new Claim("Name", user.Profile.Name),
                    new Claim("Age", user.Profile.Age.ToString() ?? string.Empty),
                    new Claim(ClaimValueTypes.Email, user.Email),
                    new Claim("PhoneNumber", user.Profile.PhoneNumber),
                    new Claim("PictureURL", user.Profile.PictureURL ?? string.Empty),
                    new Claim("BirthDay", user.Profile.BirthDay.ToString() ?? string.Empty),
                    new Claim("JoinDate", user.Profile.joinDate.ToString()),
                    new Claim(ClaimTypes.Role, userRole)

                }
                ),
                Expires = DateTime.UtcNow.AddHours(12),
                Issuer = _configuration["JwtConfig:Issuer"],
                Audience = _configuration["JwtConfig:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secetkeyBytes), SecurityAlgorithms.HmacSha512Signature)

            };
                var token = jwtTokenHandler.CreateToken(tokenDesciptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
