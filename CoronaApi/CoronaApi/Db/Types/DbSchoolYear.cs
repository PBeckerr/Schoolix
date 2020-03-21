using System;
using System.Collections.Generic;

namespace CoronaApi.Db.Types
{
    public class DbSchoolYear
    {
        public DbSchoolYear()
        {
            Classes = new HashSet<DbSchoolClass>();
        }
        
        public Guid Id { get; set; }

        public DateTime Begin { get; set; }
        
        public DateTime End { get; set; }
        
        public virtual ICollection<DbSchoolClass> Classes { get; set; }
        
        // TODO: Add School
    }
}
