using System;
using System.Collections.Generic;
using UniqueTodoApplication.Enum;

namespace UniqueTodoApplication.Entities
{
    public class Todoitem : BaseEntity
    {
        public string Name{get;set;}

        public Status Status{get;set;}

        public string Description{get;set;}

        public int CustomerId{get;set;}

        public Priority Priority{get;set;}

        public TimeSpan TimeInterval{get;set;}

        public DateTime StartingTime{get;set;}

        public DateTime OriginalTime{get;set;}

        public List<Reminder> Reminder{get;set;} = new List<Reminder>();
    }
}