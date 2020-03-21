using System;

namespace CoronaApi.Db.Types
{
    public class DbSchoolClass
    {
        public Guid Id { get; set; }

        public byte Grade { get; set; }
        
        public string Label { get; set; }
    }
}
