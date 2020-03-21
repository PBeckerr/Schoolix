using System;
using System.Collections.Generic;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class SchoolYearDto : IMapFrom<DbSchoolYear>
    {
        public Guid Id { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public List<SchoolClassDto> Classes { get; set; }
        
        public Guid SchoolId { get; set; }
    }
}
