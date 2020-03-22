using System;

namespace CoronaApi.Db.Types
{
    internal interface IHasId
    {
        public Guid Id { get; set; }
    }
}