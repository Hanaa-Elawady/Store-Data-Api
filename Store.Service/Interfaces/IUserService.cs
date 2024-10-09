using Store.Service.Dtos.Users;

namespace Store.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Login(LoginDto input);
        Task<UserDto> Register(RegisterDto input);
    }
}
