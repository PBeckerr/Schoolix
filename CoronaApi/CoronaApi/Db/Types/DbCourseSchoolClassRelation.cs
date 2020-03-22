using System;
using CoronaApi.Db.ContextSeeding;

namespace CoronaApi.Db.Types
{
    public class DbCourseSchoolClassRelation
    {
        public Guid CourseId { get; set; }

        public DbCourse Course { get; set; }

        public Guid ClassId { get; set; }

        public DbSchoolClass Class { get; set; }
    }
}
