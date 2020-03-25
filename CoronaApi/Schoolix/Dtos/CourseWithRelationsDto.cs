using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Schoolix.Db.Types;
using Schoolix.Mapping;
using Schoolix.Models;

namespace Schoolix.Dtos
{
    public class CourseWithRelationsDto : IMapFrom<DbCourse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<SchoolClassDto> SchoolClasses { get; set; }

        public List<ExerciseDto> Exercises { get; set; }

        public Guid SubjectId { get; set; }

        public SubjectDto Subject { get; set; }

        public List<ApplicationUserDto> Students { get; set; }

        public string TeacherId { get; set; }

        public ApplicationUserDto Teacher { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbCourse, CourseWithRelationsDto>()
                .ForMember(e => e.SchoolClasses, o => o.MapFrom(e => e.ClassRelations.Select(cr => cr.Class)))
                .ForMember(e => e.Students, o => o.MapFrom(e => e.StudentRelations.Select(e => e.Student)));
        }
    }
}
