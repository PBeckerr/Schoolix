using System;
using System.Collections.Generic;
using Schoolix.Db.ContextSeeding;
using Schoolix.Models;

namespace Schoolix.Db.Types
{
    public class DbSubmission: IHasId
    {
        public DbSubmission()
        {
            SubmissionFiles = new HashSet<DbSubmissionFile>();
        }

        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid ExerciseId { get; set; }

        public virtual DbExercise Exercise { get; set; }

        public virtual HashSet<DbSubmissionFile> SubmissionFiles { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }
    }
}
