using System;
using Schoolix.Db.ContextSeeding;
using Schoolix.Models;

namespace Schoolix.Db.Types
{
    public class DbCourseStudentRelation
    {
        public Guid CourseId { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }
    }
}
