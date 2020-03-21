using System;

namespace CoronaApi.Db.Types
{
    public class DbExerciseFile
    {
        public Guid Id { get; set; }

        public Guid ExerciseId { get; set; }

        public Guid FileId { get; set; }
        
        public DbFile File { get; set; }
    }
}
