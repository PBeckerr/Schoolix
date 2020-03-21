using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbCourse, CourseDto>()
                .ForMember(e => e.Classes, o => o.MapFrom(e => e.ClassRelations.Select(cr => cr.Class)));
        }
    }

    public class CourseDto : IMapFrom<DbCourse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
