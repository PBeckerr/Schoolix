using System;
using CoronaApi.Db.ContextSeeding;

namespace CoronaApi.Db.Types
{
    public class DbFile: IHasId
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
