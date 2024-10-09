using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities.Identity;
using Store.Service.Dtos.Users;
using Store.Service.HandleResponse;
using Store.Service.Interfaces;

namespace Store.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Injection
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        #endregion
        #region Login
        [HttpPost]
        public async Task<ActionResult<AppUser>> Login(LoginDto input)
        {
            var user = await _userService.Login(input);
            if (user is null)
                return BadRequest(new CustomExeption(400 , "Login Error"));

            return Ok(user);

        }
        #endregion

        #region Register
        [HttpPost]
        public async Task<ActionResult<AppUser>> Register(RegisterDto input)
        {
            var user = await _userService.Register(input);
            if (user is null)
                return BadRequest(new CustomExeption(400, "register Error"));

            return Ok(user);

        }
        #endregion

        #region getUserDetails
        [HttpGet]
        [Authorize]
        public async Task<UserDto> getUserDetails()
        {
            var userId = User?.FindFirst("UserId");

            var Data = await _userManager.FindByIdAsync(userId.Value);

            return new UserDto
            {
                Email = Data.Email,
                Id = Guid.Parse(Data.Id),
                DisplayName = Data.DisplayName,
            };
        }
        #endregion
    }
}
