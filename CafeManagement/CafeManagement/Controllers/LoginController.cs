using CafeManagement.Dtos.Request;
using CafeManagement.Interfaces.Services;
using CafeManagement.Interfaces.Services.Login;
using CafeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase   
    {
        private ITokenService _tokenService;
        private IUserService _userService;
        public LoginController(ITokenService tokenService,IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }
        [HttpPost]
        public IActionResult Login([FromBody]LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(_tokenService.Validate(request));
        }


    }
}
