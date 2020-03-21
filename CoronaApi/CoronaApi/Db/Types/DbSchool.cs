using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public ApplicationUser Owner { get; set; }

        public virtual HashSet<DbSubject> Subjects { get; set; }

        public virtual HashSet<DbSchoolYear> SchoolYears { get; set; }

        public virtual HashSet<ApplicationUser> Users { get; set; }
    }
}
