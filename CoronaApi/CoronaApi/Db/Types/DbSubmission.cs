using System;
using System.Collections.Generic;

namespace CoronaApi.Db.Types
{
    public class DbSubmission
    {
        public DbSubmission()
        {
            Files = new HashSet<DbFile>();
        }
        
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        
        public virtual DbExercise Exercise { get; set; }

        public virtual ICollection<DbFile> Files { get; set; }
        
        // TODO: Add Student
    }
}
