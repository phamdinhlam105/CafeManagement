using CafeManagement.Dtos.Request;
using CafeManagement.Models;
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
        private readonly UserManager<User> _userManager;
        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User created successfully" });
            }

            return BadRequest(result.Errors);
        }
    }
}
