using System;

namespace UniqueTodoApplication.Entities
{
    public abstract class BaseEntity
    {
        public int Id{get;set;}

        public bool IsDeleted { get; set; } 

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime? Modified { get; set; }

        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }
    }
}