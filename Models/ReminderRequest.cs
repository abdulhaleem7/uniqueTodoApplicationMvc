using System;

namespace UniqueTodoApplication.Models
{
    public class ReminderRequest
    {
        public string FirstName{get;set;}

       public string ToEmail{get;set;}
       
       public string Name{get;set;}

       public DateTime OriginalTime{get;set;}
    }
}