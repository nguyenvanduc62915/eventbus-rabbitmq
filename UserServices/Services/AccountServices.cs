using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserServices.Configurations;
using UserServices.Dtos;
using UserServices.Interface;
using UserServices.Models;

namespace UserServices.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<Onwer> _userManager;
        private readonly SignInManager<Onwer> _signInManager;
        private readonly JwtSetting _jwtSetting;
        public AccountServices(IAccountRepository accountRepository,
            UserManager<Onwer> userManager,
            SignInManager<Onwer> signInManager,
            IOptions<JwtSetting> options)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSetting = options.Value;
        }
        public async Task<AuthenResponse> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey));

                var token = new JwtSecurityToken
                (
                    issuer: _jwtSetting.ValidIssuer,
                    audience: _jwtSetting.ValidAudience,
                    expires: DateTime.Now.AddHours(30),
                    claims: claims,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );

                return new AuthenResponse
                {
                    IsSuccess = true,
                    Message = "Login Success!",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                };
            }

            return new AuthenResponse
            {
                IsSuccess = false,
                Message = "Login Fail! Incorrect Email or Password"
            };
        }

        public async Task<AuthenResponse> Register(RegisterDto registerDto)
        {
            var userExist = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userExist != null)
            {
                return new AuthenResponse
                {
                    IsSuccess = false,
                    Message = "Email is exist!"
                };
            }

            var user = new Onwer
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result
                    .Errors
                    .Select(e => e.Description));
                return new AuthenResponse
                {
                    IsSuccess = false,
                    Message = $"Register Fail! Error: {errors}"
                };
            }

            return new AuthenResponse
            {
                IsSuccess = true,
                Message = "Login Success!"
            };
        }
    }
}
