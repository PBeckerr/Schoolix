using System;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class CourseDto : IMapFrom<DbCourse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}