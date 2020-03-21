using System;
using System.Collections.Generic;
using AutoMapper;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class SubmissionDto : IMapFrom<DbSubmission>
    {
        public SubmissionDto()
        {
            Files = new HashSet<FileDto>();
        }
        
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        
        public Guid ExerciseId { get; set; }
        
        public ExerciseDto Exercise { get; set; }

        public HashSet<FileDto> Files { get; set; }
        
        // TODO: Add Student
    }
}
