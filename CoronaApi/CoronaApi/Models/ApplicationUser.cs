using Microsoft.AspNetCore.Identity;

namespace CoronaApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        School,
        Teacher,
        Student
    }
}