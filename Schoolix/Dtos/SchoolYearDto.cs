using System;
using System.Collections.Generic;
using Schoolix.Db.Types;
using Schoolix.Mapping;

namespace Schoolix.Dtos
{
    public class SchoolYearDto : IMapFrom<DbSchoolYear>
    {
        public Guid Id { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public List<SchoolClassDto> Classes { get; set; }
        
        public Guid SchoolId { get; set; }

        public string Name => $"{Begin.Year}/{End.Year}";
    }
}
