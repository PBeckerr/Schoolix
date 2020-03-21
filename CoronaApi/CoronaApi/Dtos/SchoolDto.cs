using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;
using CoronaApi.Models;

namespace CoronaApi.Dtos
{
    public class SchoolDto : IMapFrom<DbSchool>
    {
        public SchoolDto()
        {
            Subjects = new List<SubjectDto>();
            SchoolYears = new List<SchoolYearDto>();
        }
        
        public Guid Id { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public ApplicationUser Owner { get; set; }
        
        public List<SubjectDto> Subjects { get; set; }
        
        public List<SchoolYearDto> SchoolYears { get; set; }
        
        public List<ApplicationUserDto> Teachers { get; set; }
        
        public List<ApplicationUserDto> Students { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbSchool, SchoolDto>()
                .ForMember(e => e.Teachers, o => o.MapFrom(e => e.Users.Where(u => u.UserType == UserType.Teacher)))
                .ForMember(e => e.Students, o => o.MapFrom(e => e.Users.Where(u => u.UserType == UserType.Student)));
        }
    }
}
