using System;

namespace CoronaApi.Db.Types
{
    public class DbSubject
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Guid SchoolId { get; set; }
    }
}
