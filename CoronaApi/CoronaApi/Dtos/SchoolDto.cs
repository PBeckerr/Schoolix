using System;
using System.Collections.Generic;
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
        
        public virtual List<SubjectDto> Subjects { get; set; }
        
        public virtual List<SchoolYearDto> SchoolYears { get; set; }
        
        // TODO: Add Students
    }
}
