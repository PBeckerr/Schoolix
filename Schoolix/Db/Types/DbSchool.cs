using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Schoolix.Db.ContextSeeding;
using Schoolix.Models;

namespace Schoolix.Db.Types
{
    public class DbSchool : IHasId
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
        public string Name { get; set; }
    }
}
