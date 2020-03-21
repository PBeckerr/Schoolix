using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class ExerciseDto : IMapFrom<DbExercise>
    {
        public ExerciseDto()
        {
            Files = new HashSet<FileDto>();
            Submissions = new HashSet<SubmissionDto>();
        }
        
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime ExpirationDate { get; set; }
        
        public Guid CourseId { get; set; }
        
        public CourseDto Course { get; set; }

        public HashSet<FileDto> Files { get; set; }
        
        public HashSet<SubmissionDto> Submissions { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbExercise, ExerciseDto>()
                .ForMember(x => x.Files, o => o.MapFrom(x => x.ExerciseFiles.Select(sf => sf.File)));
        }
    }
}
