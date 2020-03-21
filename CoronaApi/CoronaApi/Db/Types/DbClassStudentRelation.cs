using System;
using CoronaApi.Models;

namespace CoronaApi.Db.Types
{
    public class DbClassStudentRelation
    {
        public Guid ClassId { get; set; }
        
        public Guid StudentId { get; set; }
        
        public virtual ApplicationUser Student { get; set; }
    }
}
