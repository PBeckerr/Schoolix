using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CoronaApi.Db.Types;
using CoronaApi.Models;
using Microsoft.AspNetCore.Identity;

namespace CoronaApi.Db.ContextSeeding
{
    public static class ContextSeed
    {
        public static void EnsureSeedDataForContext(this ApplicationDbContext context, IUserStore<ApplicationUser> userManager)
        {
            // context.SaveChanges();
        }

        private static void ApplySeeding<T>(ApplicationDbContext context, List<T> seedData) where T : class, IHasId
        {
            var ids = seedData.Select(e => e.Id);
            var inDb = context.Set<T>()
                              .Where(dbEntity => ids.Contains(dbEntity.Id))
                              .Select(dbEntity => dbEntity.Id)
                              .ToList();
            var idToInsert = ids.Except(inDb);

            context.Set<T>()
                   .AddRange(seedData.Where(seed => idToInsert.Contains(seed.Id)));
        }
    }
}