using System;
using System.Collections.Generic;
using CoronaApi.Db.Types;
using CoronaApi.Models;

namespace CoronaApi.Dtos
{
    public class SchoolDto
    {
        public SchoolDto()
        {
            Subjects = new HashSet<SubjectDto>();
            SchoolYears = new HashSet<SchoolYearDto>();
        }
        
        public Guid Id { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public ApplicationUser Owner { get; set; }
        
        public virtual HashSet<SubjectDto> Subjects { get; set; }
        
        public virtual HashSet<SchoolYearDto> SchoolYears { get; set; }
    }
}
