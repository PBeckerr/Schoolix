using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class SchoolClassDto : IMapFrom<DbSchoolClass>
    {
        public SchoolClassDto()
        {
            Courses = new HashSet<CourseDto>();
        }

        public Guid Id { get; set; }

        public byte Grade { get; set; }

        public string Label { get; set; }

        public Guid SchoolYearId { get; set; }

        public SchoolYearDto SchoolYear { get; set; }

        public HashSet<CourseDto> Courses { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbSchoolClass, SchoolClassDto>()
                .ForMember(e => e.Courses, o => o.MapFrom(e => e.CourseRelations.Select(cr => cr.Course)));
        }
    }
}
