using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.Identity;
using Store.Service.Dtos.Users;
using Store.Service.Interfaces;

namespace Store.Service.Services
{
    public class UserService : IUserService
    {
        #region Injection
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(SignInManager<AppUser> signInManager , UserManager<AppUser> userManager , ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        #endregion

        #region Login
        public async Task<UserDto> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password,false);
            if (!result.Succeeded)
                throw new Exception("LoginError");
            
            return new UserDto()
            {
                Email = input.Email,
                Id = Guid.Parse(user.Id),
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateUserToken(user)
            };

        }
        #endregion

        #region Register 
        public async Task<UserDto> Register(RegisterDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user is not null)
                throw new Exception("Email Already exist");

            var appUser = new AppUser
            {
                Email = input.Email,
                UserName = input.DisplayName,
                DisplayName = input.DisplayName,
            };

            var result = await _userManager.CreateAsync(appUser);
            if (!result.Succeeded)
                throw new Exception("Register Error");

            return new UserDto
            {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Id = Guid.Parse(appUser.Id),
                Token = _tokenService.GenerateUserToken(appUser)
            };
        }
        #endregion
    }
}
