using System;
using System.Collections.Generic;
using Schoolix.Db.ContextSeeding;

namespace Schoolix.Db.Types
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
