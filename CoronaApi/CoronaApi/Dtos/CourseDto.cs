using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class CourseDto : IMapFrom<DbCourse>
    {
        public CourseDto()
        {
            Classes = new HashSet<SchoolClassDto>();
            Exercises = new HashSet<ExerciseDto>();
        }
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public HashSet<SchoolClassDto> Classes { get; set; }
        
        public HashSet<ExerciseDto> Exercises { get; set; }
        
        // TODO: Add Students
        
        // TODO: Add Teacher
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbCourse, CourseDto>()
                .ForMember(e => e.Classes, o => o.MapFrom(e => e.ClassRelations.Select(cr => cr.Class)));
        }
    }
}
