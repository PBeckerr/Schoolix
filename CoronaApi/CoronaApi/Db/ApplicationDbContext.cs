using CoronaApi.Db.Types;
using CoronaApi.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoronaApi.Db
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        
        public DbSet<DbCourse> Courses { get; set; }

        public DbSet<DbExercise> Exercises { get; set; }
        
        public DbSet<DbExerciseFile> ExerciseFiles { get; set; }

        public DbSet<DbFile> Files { get; set; }

        public DbSet<DbSchoolClass> Classes { get; set; }
        
        public DbSet<DbSchoolYear> SchoolYears { get; set; }

        public DbSet<DbSubmission> Submissions { get; set; }

        public DbSet<DbSubmissionFile> SubmissionFiles { get; set; }
    }
}
