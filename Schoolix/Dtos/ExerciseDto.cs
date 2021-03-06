using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Schoolix.Db.Types;
using Schoolix.Mapping;

namespace Schoolix.Dtos
{
    public class ExerciseDto : IMapFrom<DbExercise>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid CourseId { get; set; }

        public CourseDto Course { get; set; }

        public List<FileDto> Files { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbExercise, ExerciseDto>()
                .ForMember(x => x.Files,
                           o => o.MapFrom(x => x.ExerciseFiles.Select(sf => sf.File)));
        }
    }

    public class ExerciseWithSubmissionDto : IMapFrom<DbExercise>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid CourseId { get; set; }

        public CourseDto Course { get; set; }

        public List<FileDto> Files { get; set; }

        public List<SubmissionDto> Submissions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbExercise, ExerciseWithSubmissionDto>()
                   .ForMember(x => x.Files,
                              o => o.MapFrom(x => x.ExerciseFiles.Select(sf => sf.File)));
        }
    }
}
