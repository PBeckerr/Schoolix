using System;
using CoronaApi.Db.Types;
using CoronaApi.Mapping;

namespace CoronaApi.Dtos
{
    public class FileDto : IMapFrom<DbFile>
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
