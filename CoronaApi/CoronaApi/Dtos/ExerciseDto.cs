using System;
using System.Collections.Generic;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class ExerciseDto : IMapFrom<DbExercise>
    {
        public ExerciseDto()
        {
            Files = new HashSet<FileDto>();
        }
        
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime ExpirationDate { get; set; }
        
        public Guid CourseId { get; set; }
        
        public CourseDto Course { get; set; }

        public HashSet<FileDto> Files { get; set; }
    }
}
