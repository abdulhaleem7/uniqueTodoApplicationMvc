using System;
using System.Collections.Generic;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Enum;

namespace UniqueTodoApplication.DTOs
{
    public class TodoitemDto
    {
        public int Id{get;set;}

        public string Name{get;set;}

        public Status Status{get;set;}

        public string Description{get;set;}

        public int CustomerId{get;set;}

        public Priority Priority{get;set;}

        public TimeSpan TimeInterval{get;set;}

        public DateTime StartingTime{get;set;}

        public DateTime OriginalTime{get;set;}

        public DateTime? Modified { get; set; }

        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }

        public List<Reminder> Reminder{get;set;} = new List<Reminder>();
    }
}