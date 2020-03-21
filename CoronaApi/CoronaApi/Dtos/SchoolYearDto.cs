using System;
using System.Collections.Generic;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class SchoolYearDto : IMapFrom<DbSchoolYear>
    {
        public SchoolYearDto()
        {
            Classes = new HashSet<SchoolClassDto>();
        }
        
        public Guid Id { get; set; }

        public DateTime Begin { get; set; }
        
        public DateTime End { get; set; }
        
        public HashSet<SchoolClassDto> Classes { get; set; }
        
        // TODO: Add School
    }
}
