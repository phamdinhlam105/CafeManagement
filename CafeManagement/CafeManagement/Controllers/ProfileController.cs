using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeManagement.Controllers
{
    [Authorize]
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
                return Ok(newProfile);
            }
            catch
            {
                return BadRequest(new ErrorResponse { Error = 401, Message = "Cant update" });
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var profile = _userService.GetProfileById(userId);
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
            catch
            {
                return BadRequest();
            }
        }
    }
}
