using System.Threading.Tasks;
using AutoMapper;
using CoronaApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoronaApi.Controllers
{
    public class RegisterController : BaseV1ApiController
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterController(IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<IdentityResult>> Register(ApplicationUserDto applicationUserDto)
        {
            var applicationUser = this._mapper.Map<ApplicationUser>(applicationUserDto);
            var identity = await this._userManager.CreateAsync(applicationUser);
            await this._signInManager.SignInAsync(applicationUser, false);
            return this.Ok();
        }
    }

    public class LoginController : BaseV1ApiController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(SignInManager<ApplicationUser> signInManager)
        {
            this._signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<IdentityResult>> Register(LoginDto loginDto)
        {
            await this._signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, true, false);
            return this.LocalRedirect("/authentication/logout-callback");
        }
    }
}