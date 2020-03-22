using System;
using System.Collections.Generic;
using CoronaApi.Db.ContextSeeding;

namespace CoronaApi.Db.Types
{
    public class DbSchoolYear: IHasId
    {
        public DbSchoolYear()
        {
            Classes = new HashSet<DbSchoolClass>();
        }

        public Guid Id { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public virtual HashSet<DbSchoolClass> Classes { get; set; }

        public Guid SchoolId { get; set; }
    }
}
