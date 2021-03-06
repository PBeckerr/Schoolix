using System;
using System.Collections.Generic;
using Schoolix.Db.ContextSeeding;
using Schoolix.Models;

namespace Schoolix.Db.Types
{
    public class DbCourse: IHasId
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

        public virtual HashSet<DbCourseStudentRelation> StudentRelations { get; set; }

        public string TeacherId { get; set; }

        public virtual ApplicationUser Teacher { get; set; }
    }
}
