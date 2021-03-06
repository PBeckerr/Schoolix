using System;
using System.Collections.Generic;
using Schoolix.Db.ContextSeeding;
using Schoolix.Models;

namespace Schoolix.Db.Types
{
    public class DbSchoolClass: IHasId
    {
        public DbSchoolClass()
        {
            CourseRelations = new HashSet<DbCourseSchoolClassRelation>();
        }

        public Guid Id { get; set; }

        public byte Grade { get; set; }

        public string Label { get; set; }

        public Guid SchoolYearId { get; set; }

        public virtual DbSchoolYear SchoolYear { get; set; }

        public virtual HashSet<DbCourseSchoolClassRelation> CourseRelations { get; set; }

        public virtual HashSet<DbClassStudentRelation> StudentRelations { get; set; }
    }
}
