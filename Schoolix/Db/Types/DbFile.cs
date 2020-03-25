using System;
using Schoolix.Db.ContextSeeding;

namespace Schoolix.Db.Types
{
    public class DbFile: IHasId
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
