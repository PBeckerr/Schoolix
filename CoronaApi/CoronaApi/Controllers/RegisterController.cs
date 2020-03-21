using System;
using System.Threading.Tasks;
using AutoMapper;
using CoronaApi.Identity;
using CoronaApi.Mapping;
using CoronaApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoronaApi.Controllers
{
    public class RegisterController : BaseV1ApiController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterController(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<IdentityResult>> Register(ApplicationUserDto applicationUserDto)
        {
            var applicationUser = this._mapper.Map<ApplicationUser>(applicationUserDto);
            var identity = await this._userManager.CreateAsync(applicationUser);
            return Ok(identity);
        }
    }

    public class ApplicationUserDto : IMapFrom<ApplicationUser>
    {
        public UserType UserType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}