using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CoronaApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoronaApi.Identity
{
    public class SchoolUserManager : UserManager<ApplicationUser>
    {
        private readonly IMemoryCache _memoryCache;

        public SchoolUserManager(IMemoryCache memoryCache, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
                                 IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
                                 IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
                                 IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(
            store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger)
        {
            this._memoryCache = memoryCache;
        }

        public override string GetUserId(ClaimsPrincipal principal)
        {
            var userId = principal.Claims.SingleOrDefault(claim => claim.Type == "userId")?.Value;
            if (userId != null)
            {
                return userId;
            }

            return base.GetUserId(principal);
        }

        public override async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            var userId = this.GetUserId(principal);
            return await this._memoryCache.GetOrCreateAsync(userId,
                                                            async entry =>
                                                            {
                                                                var user = await this.Store.FindByIdAsync(userId, CancellationToken.None);
                                                                entry.Value = user;
                                                                entry.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(2);
                                                                return user;
                                                            });
        }
    }
}