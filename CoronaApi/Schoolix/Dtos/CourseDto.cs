using System;
using Schoolix.Db.Types;
using Schoolix.Mapping;

namespace Schoolix.Dtos
{
    public class CourseDto : IMapFrom<DbCourse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}