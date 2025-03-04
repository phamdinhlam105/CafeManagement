using System;
using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Enums;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Interfaces.Services.Login;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace CafeManagement.Services.Login
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ITokenService _tokenService;

        public UserService(IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task Signup(SignupRequest request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {
                var profile = new Profile
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Name = request.Name,
                    Email = request.Email,
                    BirthDay = request.BirthDay,
                    PhoneNumber = request.PhoneNumber,
                    joinDate = DateTime.UtcNow
                };
                if(request.BirthDay!=null)
                {
                    var today = DateOnly.FromDateTime(DateTime.UtcNow);
                    int age = today.Year - request.BirthDay.Value.Year;
                    profile.Age = age;
                }
                if (!await _roleManager.RoleExistsAsync(Role.Employee))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Role.Employee));
                }
                await _userManager.AddToRoleAsync(user, Role.Employee);
                await _unitOfWork.Profile.Add(profile);
                user.Profile = profile;
                await _userManager.UpdateAsync(user);
            }   
        }

        public async Task Update(User user)
        {
            if (user != null)
                await _userManager.UpdateAsync(user);  
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {

            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return null;
            var userAccount = await _userManager.FindByNameAsync(request.UserName);
            if (userAccount == null)
                return null;
           
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(userAccount, userAccount.PasswordHash, request.Password);

            if (result != PasswordVerificationResult.Success)
                return null;
            var profile = await _unitOfWork.Profile.GetByUserId(userAccount.Id);
            if (profile == null)
                profile = new Profile
                {
                    Id = Guid.NewGuid(),
                    Name = request.UserName,
                    Email = userAccount.Email,
                    PhoneNumber="111111",
                    joinDate=DateTime.Now,

                };
            await _unitOfWork.Profile.Add(profile);
            userAccount.Profile = profile;
            var token = await _tokenService.CreateToken(userAccount);
            return new LoginResponse
            {
                UserName = request.UserName,
                ExpiresIn = 180,
                AccessToken = token
            };
        }

        public async Task ChangeRole(SetRoleRequest request)
        {

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User không được null");
            }

            if (!Enum.IsDefined(typeof(UserRole), request.Role))
            {
                throw new ArgumentException("Vai trò không hợp lệ", nameof(request.Role));
            }

            string newRole = Role.GetRoleName(request.Role);

            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(newRole));
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, newRole);
        }
    }
}
