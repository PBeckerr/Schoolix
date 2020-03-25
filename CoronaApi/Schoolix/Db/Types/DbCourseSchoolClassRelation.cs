using System;
using Schoolix.Db.ContextSeeding;

namespace Schoolix.Db.Types
{
    public class DbCourseSchoolClassRelation
    {
        public Guid CourseId { get; set; }

        public DbCourse Course { get; set; }

        public Guid ClassId { get; set; }

        public DbSchoolClass Class { get; set; }
    }
}
