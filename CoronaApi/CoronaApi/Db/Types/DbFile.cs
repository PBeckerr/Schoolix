using System;

namespace CoronaApi.Db.Types
{
    public class DbFile
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
