using System;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;
using Microsoft.AspNetCore.Identity;

namespace CoronaApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid SchoolId { get; set; }

        public UserType UserType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DbSchool School { get; set; }
    }

    public class ApplicationUserDto : IMapFrom<ApplicationUser>
    {
        public UserType UserType { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public enum UserType
    {
        School,
        Teacher,
        Student
    }
}
