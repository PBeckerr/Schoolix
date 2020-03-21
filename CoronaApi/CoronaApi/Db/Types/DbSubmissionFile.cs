using System;

namespace CoronaApi.Db.Types
{
    public class DbSubmissionFile
    {
        public Guid SubmissionId { get; set; }

        public Guid FileId { get; set; }

        public DbFile File { get; set; }

        public Guid Id { get; set; }
    }
}
