using System;

namespace Schoolix.Db.Types
{
    internal interface IHasId
    {
        public Guid Id { get; set; }
    }
}