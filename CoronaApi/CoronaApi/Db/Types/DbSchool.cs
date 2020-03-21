using System;
using System.Collections.Generic;
using CoronaApi.Models;

namespace CoronaApi.Db.Types
{
    public class DbSchool
    {
        public DbSchool()
        {
            Subjects = new HashSet<DbSubject>();
            SchoolYears = new HashSet<DbSchoolYear>();
        }
        
        public Guid Id { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public ApplicationUser Owner { get; set; }
        
        public virtual HashSet<DbSubject> Subjects { get; set; }
        
        public virtual HashSet<DbSchoolYear> SchoolYears { get; set; }
        
        // TODO: Add Teachers and Students
    }
}
