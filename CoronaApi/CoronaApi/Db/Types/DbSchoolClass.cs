using System;
using System.Collections.Generic;

namespace CoronaApi.Db.Types
{
    public class DbSchoolClass
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
        
        public virtual ICollection<DbCourseSchoolClassRelation> CourseRelations { get; set; }
    }
}
