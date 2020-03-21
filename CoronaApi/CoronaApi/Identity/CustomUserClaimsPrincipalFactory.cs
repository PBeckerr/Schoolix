using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CoronaApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoronaApi.Identity
{
    public class SchoolUserManager : UserManager<ApplicationUser>
    {
        public SchoolUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }

    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var userId = await this.UserManager.GetUserIdAsync(user);
            var userName = await this.UserManager.GetUserNameAsync(user);
            var id = new ClaimsIdentity("Identity.Application",
                                        this.Options.ClaimsIdentity.UserNameClaimType,
                                        this.Options.ClaimsIdentity.RoleClaimType);
            id.AddClaim(new Claim(this.Options.ClaimsIdentity.UserIdClaimType, userId));
            id.AddClaim(new Claim(this.Options.ClaimsIdentity.UserNameClaimType, user.UserName));
            id.AddClaim(new Claim("preferred_username", userName));
            if (this.UserManager.SupportsUserSecurityStamp)
            {
                id.AddClaim(new Claim(this.Options.ClaimsIdentity.SecurityStampClaimType,
                                      await this.UserManager.GetSecurityStampAsync(user)));
            }

            if (this.UserManager.SupportsUserClaim)
            {
                id.AddClaims(await this.UserManager.GetClaimsAsync(user));
            }

            id.AddClaim(new Claim("userType", user.UserType.ToString()));
            return id;
        }

        public CustomUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }
    }
}