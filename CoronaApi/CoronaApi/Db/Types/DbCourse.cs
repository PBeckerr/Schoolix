using System;
using System.Collections.Generic;

namespace CoronaApi.Db.Types
{
    public class DbCourse
    {
        public DbCourse()
        {
            ClassRelations = new HashSet<DbCourseSchoolClassRelation>();
            Exercises = new HashSet<DbExercise>();
        }
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual HashSet<DbCourseSchoolClassRelation> ClassRelations { get; set; }
        
        public virtual HashSet<DbExercise> Exercises { get; set; }
        
        public Guid SubjectId { get; set; }
        
        public virtual DbSubject Subject { get; set; }
        
        // TODO: Add Students
        
        // TODO: Add Teacher
    }
}
