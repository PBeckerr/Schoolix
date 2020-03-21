using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;
using CoronaApi.Models;

namespace CoronaApi.Dtos
{
    public class CourseWithRelationsDto : IMapFrom<DbCourse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<SchoolClassDto> Classes { get; set; }

        public List<ExerciseDto> Exercises { get; set; }

        public Guid SubjectId { get; set; }
        
        public virtual SubjectDto Subject { get; set; }

        public virtual List<ApplicationUserDto> Students { get; set; }

        public Guid TeacherId { get; set; }
        
        public ApplicationUserDto Teacher { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbCourse, CourseWithRelationsDto>()
                .ForMember(e => e.Classes, o => o.MapFrom(e => e.ClassRelations.Select(cr => cr.Class)))
                .ForMember(e => e.Students, o => o.MapFrom(e => e.StudentRelations.Select(e => e.Student)));
        }
    }
}
