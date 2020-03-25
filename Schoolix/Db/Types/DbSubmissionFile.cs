using System;

namespace Schoolix.Db.Types
{
    public class DbSubmissionFile: IHasId
    {
        public Guid SubmissionId { get; set; }

        public Guid FileId { get; set; }

        public DbFile File { get; set; }

        public Guid Id { get; set; }
    }
}
