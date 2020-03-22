using System;
using CoronaApi.Db.ContextSeeding;
using CoronaApi.Models;

namespace CoronaApi.Db.Types
{
    public class DbCourseStudentRelation
    {
        public Guid CourseId { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }
    }
}
