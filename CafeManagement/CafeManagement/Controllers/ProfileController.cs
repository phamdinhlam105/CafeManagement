using CafeManagement.Dtos.Request.UserReq;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Services.Login;
using CafeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeManagement.Controllers
{
    [Authorize(Policy = "NotCustomer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        public ProfileController(IUserService userService)
        {
            _userService = userService; 
        }

        [HttpPost("profile")]
        public async Task<IActionResult> EditProfile([FromBody]Profile updatedProfile)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) 
                return NotFound();
            try
            {
                var newProfile = await _userService.UpdateProfile(updatedProfile, userId);
                if (newProfile == null)
                    return NotFound("Not found user");
                return Ok(newProfile);
            }
            catch
            {
                return BadRequest(new { Error = 401, Message = "Cant update" });
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var profile = await _userService.GetProfileById(userId);
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }

        [HttpPost("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return NotFound();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                await _userService.ChangePassword(request, userId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
