using System;
using Schoolix.Db.ContextSeeding;

namespace Schoolix.Db.Types
{
    public class DbSubject: IHasId
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid SchoolId { get; set; }
    }
}
