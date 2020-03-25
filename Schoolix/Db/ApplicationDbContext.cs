using Schoolix.Db.Types;
using Schoolix.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Schoolix.Db
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<DbClassStudentRelation> ClassStudentRelations { get; set; }

        public DbSet<DbCourse> Courses { get; set; }

        public DbSet<DbCourseSchoolClassRelation> CourseClassRelations { get; set; }

        public DbSet<DbCourseStudentRelation> CourseStudentRelations { get; set; }

        public DbSet<DbExercise> Exercises { get; set; }

        public DbSet<DbExerciseFile> ExerciseFiles { get; set; }

        public DbSet<DbFile> Files { get; set; }

        public DbSet<DbSchool> Schools { get; set; }

        public DbSet<DbSchoolClass> Classes { get; set; }

        public DbSet<DbSchoolYear> SchoolYears { get; set; }

        public DbSet<DbSubject> Subjects { get; set; }

        public DbSet<DbSubmission> Submissions { get; set; }

        public DbSet<DbSubmissionFile> SubmissionFiles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.UserType)
                      .IsRequired();
                entity.Property(e => e.FirstName)
                      .HasMaxLength(100);
                entity.Property(e => e.LastName)
                      .HasMaxLength(100);
            });

            builder.Entity<DbClassStudentRelation>(entity => entity.HasKey(e => new {e.ClassId, e.StudentId}));

            builder.Entity<DbCourse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .IsRequired();
                entity.HasMany(e => e.ClassRelations)
                      .WithOne(e => e.Course)
                      .HasForeignKey(e => e.CourseId);
                entity.HasMany(e => e.Exercises)
                      .WithOne(e => e.Course)
                      .HasForeignKey(e => e.CourseId);
                entity.Property(e => e.TeacherId)
                      .IsRequired();
                entity.HasOne(e => e.Teacher)
                      .WithMany()
                      .HasForeignKey(e => e.TeacherId);
                entity.HasMany(e => e.StudentRelations)
                      .WithOne()
                      .HasForeignKey(e => e.CourseId);
            });

            builder.Entity<DbCourseSchoolClassRelation>(entity => entity.HasKey(e => new {e.CourseId, e.ClassId}));

            builder.Entity<DbCourseStudentRelation>(entity => entity.HasKey(e => new {e.CourseId, e.StudentId}));

            builder.Entity<DbExercise>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Course)
                      .WithMany(e => e.Exercises)
                      .HasForeignKey(e => e.CourseId);
                entity.HasMany(e => e.ExerciseFiles)
                      .WithOne()
                      .HasForeignKey(e => e.ExerciseId);
                entity.Property(e => e.Title)
                      .HasMaxLength(200);
                entity.Property(e => e.CourseId)
                      .IsRequired();
            });

            builder.Entity<DbExerciseFile>(entity =>
            {
                entity.HasKey(e => new {e.ExerciseId, e.FileId});
                entity.HasOne(e => e.File)
                      .WithMany()
                      .HasForeignKey(e => e.FileId);
            });

            builder.Entity<DbFile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .HasMaxLength(255)
                      .IsRequired();
                entity.Property(e => e.Url)
                      .HasMaxLength(2000)
                      .IsRequired();
            });

            builder.Entity<DbSchool>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Subjects)
                      .WithOne()
                      .HasForeignKey(e => e.SchoolId);
                entity.HasMany(e => e.SchoolYears)
                      .WithOne()
                      .HasForeignKey(e => e.SchoolId);
                entity.HasMany(e => e.Users)
                      .WithOne(user => user.School)
                      .HasForeignKey(e => e.SchoolId);
            });

            builder.Entity<DbSchoolClass>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Label)
                      .HasMaxLength(20)
                      .IsRequired();
                entity.Property(e => e.Grade)
                      .IsRequired();
                entity.Property(e => e.SchoolYearId)
                      .IsRequired();
                entity.HasMany(e => e.CourseRelations)
                      .WithOne(e => e.Class)
                      .HasForeignKey(e => e.ClassId);
                entity.HasOne(e => e.SchoolYear)
                      .WithMany(e => e.Classes)
                      .HasForeignKey(e => e.SchoolYearId);
                entity.HasMany(e => e.StudentRelations)
                      .WithOne()
                      .HasForeignKey(e => e.ClassId);
            });

            builder.Entity<DbSchoolYear>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Begin)
                      .HasColumnType("date")
                      .IsRequired();
                entity.Property(e => e.End)
                      .HasColumnType("date")
                      .IsRequired();
                entity.Property(e => e.SchoolId)
                      .IsRequired();
                entity.HasMany(e => e.Classes)
                      .WithOne(e => e.SchoolYear)
                      .HasForeignKey(e => e.SchoolYearId);
            });

            builder.Entity<DbSubject>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SchoolId)
                      .IsRequired();
                entity.Property(e => e.Name)
                      .HasMaxLength(50)
                      .IsRequired();
            });

            builder.Entity<DbSubmission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date)
                      .IsRequired();
                entity.HasOne(e => e.Exercise)
                      .WithMany(e => e.Submissions)
                      .HasForeignKey(e => e.ExerciseId);
                entity.HasOne(e => e.Student)
                      .WithMany()
                      .HasForeignKey(e => e.StudentId);
            });

            builder.Entity<DbSubmissionFile>(entity =>
            {
                entity.HasKey(e => new {e.SubmissionId, e.FileId});
                entity.HasOne(e => e.File)
                      .WithMany()
                      .HasForeignKey(e => e.FileId);
            });

            base.OnModelCreating(builder);
        }
    }
}