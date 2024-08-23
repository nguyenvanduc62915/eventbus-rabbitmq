using Microsoft.AspNetCore.Mvc;
using UserServices.Dtos;
using UserServices.Interface;

namespace UserServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var response = await _accountServices.Register(registerDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _accountServices.Login(loginDto);
            if (!response.IsSuccess)
            {
                return Unauthorized(response.Message);
            }

            return Ok();
        }
    }
}
