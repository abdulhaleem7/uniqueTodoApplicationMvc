using System;

namespace UniqueTodoApplication.Entities
{
    public class Reminder : BaseEntity
    {
        public int TodoitemId{get;set;}
        
        public Todoitem Todoitem{get;set;}

        public DateTime Time{get;set;}
    }
}