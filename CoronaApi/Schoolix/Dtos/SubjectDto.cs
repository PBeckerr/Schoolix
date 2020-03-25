using System;
using Schoolix.Db.Types;
using Schoolix.Mapping;

namespace Schoolix.Dtos
{
    public class SubjectDto : IMapFrom<DbSubject>
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Guid SchoolId { get; set; }
    }
}
