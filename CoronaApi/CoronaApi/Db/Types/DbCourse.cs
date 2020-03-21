using System;
using System.Collections.Generic;

namespace CoronaApi.Db.Types
{
    public class DbCourse
    {
        public DbCourse()
        {
            Classes = new HashSet<DbSchoolClass>();
            Exercises = new HashSet<DbExercise>();
        }
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<DbSchoolClass> Classes { get; set; }
        
        public virtual ICollection<DbExercise> Exercises { get; set; }
        
        // TODO: Add Students
        
        // TODO: Add Teacher
    }
}
