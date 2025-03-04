using CafeManagement.Dtos.Request;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userSerivce;
        public AccountController(IUserService userService)
        {
            _userSerivce = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignupRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _userSerivce.Signup(request);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("setrole")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> SetRole([FromBody] SetRoleRequest request )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                await _userSerivce.ChangeRole(request);
            }
            catch 
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
