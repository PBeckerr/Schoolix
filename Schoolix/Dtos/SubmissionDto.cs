using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Schoolix.Db.Types;
using Schoolix.Mapping;
using Schoolix.Models;

namespace Schoolix.Dtos
{
    public class SubmissionDto : IMapFrom<DbSubmission>
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid ExerciseId { get; set; }

        public ExerciseDto Exercise { get; set; }

        public List<FileDto> Files { get; set; }

        public string StudentId { get; set; }

        public ApplicationUserDto Student { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DbSubmission, SubmissionDto>()
                   .ForMember(x => x.Files, o => o.MapFrom(x => x.SubmissionFiles.Select(sf => sf.File)));
        }
    }
}