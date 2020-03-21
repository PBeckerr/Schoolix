using System;
using System.Collections.Generic;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class CourseWithRelationsDto : IMapFrom<DbCourse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<SchoolClassDto> Classes { get; set; }

        public List<ExerciseDto> Exercises { get; set; }

        // TODO: Add Students

        // TODO: Add Teacher
    }

    public class CourseDto : IMapFrom<DbCourse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
