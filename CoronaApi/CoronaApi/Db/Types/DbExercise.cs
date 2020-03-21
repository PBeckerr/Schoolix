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

        public virtual ICollection<DbExerciseFile> ExerciseFiles { get; set; }
    }
}
