using System;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class SubjectDto : IMapFrom<DbSubject>
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Guid SchoolId { get; set; }
    }
}
