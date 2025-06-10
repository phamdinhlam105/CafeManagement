using System;
using Azure.Core;
using CafeManagement.Dtos.Request.UserReq;
using CafeManagement.Dtos.Respone.UserRes;
using CafeManagement.Enums;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Login;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sprache;

namespace CafeManagement.Services.Login
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

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

        public async Task<RegisterResponse> Signup(SignupRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return new RegisterResponse
                {
                    status = 0,
                    message = "email"
                };
            existingUser = await _userManager.FindByNameAsync(request.UserName);
            if(existingUser!=null)
                return new RegisterResponse
                {
                    status = 0,
                    message = "userName"
                };
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
                    joinDate = DateTime.UtcNow
                };
                if(request.BirthDay!=null)
                {
                    var today = DateOnly.FromDateTime(DateTime.UtcNow);
                    int age = today.Year - request.BirthDay.Value.Year;
                    profile.Age = age;
                }
                if(request.PhoneNumber!=null)
                    profile.PhoneNumber = request.PhoneNumber;
                if (!await _roleManager.RoleExistsAsync(Role.Customer))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Role.Customer));
                }
                await _userManager.AddToRoleAsync(user, Role.Customer);
                await _unitOfWork.Profile.Add(profile);
                user.Profile = profile;
                await _userManager.UpdateAsync(user);
                return new RegisterResponse
                {
                    status = 1
                };
            }
            throw new Exception("Cant create new user");
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
            {
                profile = new Profile
                {
                    Id = Guid.NewGuid(),
                    Name = request.UserName,
                    Email = userAccount.Email,
                    joinDate = DateTime.UtcNow,
                    UserId=userAccount.Id,
                };
                await _unitOfWork.Profile.Add(profile);
            }
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

            var newRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId);

            if (newRole == null)
                throw new Exception("Not found RoleId");

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, newRole.Name);
        }

        public async Task<Profile> UpdateProfile(Profile profile, string userId)
        {
            var existingProfile = await _unitOfWork.Profile.GetByUserId(userId);

            if (existingProfile == null)
            {
                return null; 
            }
            if(profile.Email!= existingProfile.Email)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return null;
                }
                user.Email = profile.Email;
                await _userManager.UpdateAsync(user);
            }
            await _unitOfWork.Profile.Update(profile);
            return profile; 
        }
        public async Task ChangePassword(ChangePasswordRequest request, string userId)
        {
            var existingUser = await _userManager.FindByIdAsync(userId);
            var checkOldPassword = await _userManager.CheckPasswordAsync(existingUser, request.oldPassword);
            if (existingUser == null)
                throw new Exception("Id Not found");
            var result = await _userManager.ChangePasswordAsync(existingUser, request.oldPassword, request.newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("wrong password");
            }

        }

        public async Task<Profile> GetProfileById(string id)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
                return null;
            var userProfile = await _unitOfWork.Profile.GetByUserId(id);
            if(userProfile==null)
            {
                var profile = new Profile
                {
                    Id = Guid.NewGuid(),
                    UserId = id,
                    Name = existingUser.UserName,
                    Email = existingUser.Email,
                    PhoneNumber = "000",
                    joinDate = DateTime.UtcNow
                };
                await _unitOfWork.Profile.Add(profile);
                return profile;
            }
            return userProfile;

        }
        public async Task<IEnumerable<UserResponse>> GetAllUser()
        {
            var userList= await _userManager.Users.Include(u => u.Profile).ToListAsync();
            List<UserResponse> userResponses = new List<UserResponse>();
            foreach (User user in userList)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Any())
                {
                    var role = await _roleManager.FindByNameAsync(roles.First());
                    userResponses.Add(new UserResponse
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        RealName = user.Profile.Name,
                        RoleName= role.Name,
                        Role = role.Id
                    });
                }
            }
            return userResponses;

        }

        public async Task<List<IdentityRole>> GetAllRole()
        {
            return await _roleManager.Roles.ToListAsync();
        }
    }
}
