using CafeManagement.Dtos.Request.UserReq;
using CafeManagement.Dtos.Respone;
using CafeManagement.Dtos.Respone.UserRes;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Login;
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
        private readonly IUserService _userSerivce;
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
                
                return Ok(await _userSerivce.Signup(request));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpPost("setrole")]
        public async Task<IActionResult> SetRole([FromBody] SetRoleRequest request )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _userSerivce.ChangeRole(request);
                return Ok();
            }
            catch 
            {
                return BadRequest();
            }

        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpGet("alluser")]
        public async Task<IActionResult> GetUserList()
        {
            try
            {
                var userList= await _userSerivce.GetAllUser();
                return Ok(userList);
            }
            catch(Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpGet("allrole")]
        public async Task<IActionResult> GetRolesList()
        {
            try
            {
                return Ok((await _userSerivce.GetAllRole()).Select(r => new RoleResponse
                {
                    Id = r.Id,
                    RoleName = r.Name
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
