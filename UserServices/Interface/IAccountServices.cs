using UserServices.Dtos;

namespace UserServices.Interface
{
    public interface IAccountServices
    {
        Task<AuthenResponse> Register(RegisterDto registerDto);
        Task<AuthenResponse> Login(LoginDto loginDto);
    }
}
