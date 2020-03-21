using System;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class SchoolClassDto : IMapFrom<DbSchoolClass>
    {
        public Guid Id { get; set; }

        public byte Grade { get; set; }
        
        public string Label { get; set; }
        
        public Guid SchoolYearId { get; set; }
    }
}
