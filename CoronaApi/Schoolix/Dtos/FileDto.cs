using System;
using Schoolix.Db.Types;
using Schoolix.Mapping;

namespace Schoolix.Dtos
{
    public class FileDto : IMapFrom<DbFile>
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
