using System;
using CoronaApi.Db.ContextSeeding;

namespace CoronaApi.Db.Types
{
    public class DbExerciseFile : IHasId
    {
        public Guid ExerciseId { get; set; }

        public Guid FileId { get; set; }

        public DbFile File { get; set; }

        public Guid Id { get; set; }
    }
}
