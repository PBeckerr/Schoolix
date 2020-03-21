using System;
using System.Collections.Generic;

namespace CoronaApi.Dtos
{
    public class ExerciseDto
    {
        public ExerciseDto()
        {
            Files = new HashSet<FileDto>();
        }
        
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime ExpirationDate { get; set; }

        public virtual ICollection<FileDto> Files { get; set; }
    }
}
