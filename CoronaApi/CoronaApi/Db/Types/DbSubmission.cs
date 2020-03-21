using System;
using System.Collections.Generic;

namespace CoronaApi.Db.Types
{
    public class DbSubmission
    {
        public DbSubmission()
        {
            SubmissionFiles = new HashSet<DbSubmissionFile>();
        }
        
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        
        public Guid ExerciseId { get; set; }
        
        public virtual DbExercise Exercise { get; set; }

        public virtual ICollection<DbSubmissionFile> SubmissionFiles { get; set; }
        
        // TODO: Add Student
    }
}
