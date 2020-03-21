using System;
using System.Collections.Generic;

namespace CoronaApi.Db.Types
{
    public class DbExercise
    {
        public DbExercise()
        {
            ExerciseFiles = new HashSet<DbExerciseFile>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid CourseId { get; set; }

        public virtual DbCourse Course { get; set; }

        public virtual HashSet<DbExerciseFile> ExerciseFiles { get; set; }
    }
}
