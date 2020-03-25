using System;
using System.Collections.Generic;
using Schoolix.Db.ContextSeeding;

namespace Schoolix.Db.Types
{
    public class DbExercise : IHasId
    {
        public DbExercise()
        {
            ExerciseFiles = new HashSet<DbExerciseFile>();
            Submissions = new HashSet<DbSubmission>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid CourseId { get; set; }

        public virtual DbCourse Course { get; set; }

        public virtual HashSet<DbExerciseFile> ExerciseFiles { get; set; }

        public virtual HashSet<DbSubmission> Submissions { get; set; }
    }
}
