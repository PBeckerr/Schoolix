using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class SubmissionDto : IMapFrom<DbSubmission>
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid ExerciseId { get; set; }

        public ExerciseDto Exercise { get; set; }

        public List<FileDto> Files { get; set; }

        // TODO: Add Student

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbSubmission, SubmissionDto>()
                .ForMember(x => x.Files, o => o.MapFrom(x => x.SubmissionFiles.Select(sf => sf.File)));
        }
    }
}
