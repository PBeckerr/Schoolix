using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Schoolix.Db;
using Schoolix.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace Schoolix.Identity
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            this._userManager = userManager;
            this._applicationDbContext = applicationDbContext;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var user = await this._userManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>
            {
                new Claim("userType", user.UserType.ToString()),
                new Claim("userName", user.Email),
                new Claim("userId", user.Id),
                new Claim("emailConfirmed", user.EmailConfirmed.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            var user = await this._userManager.GetUserAsync(context.Subject);

            context.IsActive = user != null;
        }
    }
}