using System;
using System.ComponentModel.DataAnnotations;
using UniqueTodoApplication.Enum;

namespace UniqueTodoApplication.Models
{
    public class TodoRequestModel
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Name{get;set;}

        [Display(Name = "Todo Status")]
        public Status Status{get;set;}

        public string Description{get;set;}

        public Priority Priority{get;set;}

        public string TimeInterval{get;set;}

        public DateTime StartingTime{get;set;}

       [Required]
        public DateTime OriginalTime{get;set;}
    }

    public class UpdateTodoitemRequestModel
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Name{get;set;}

        public string Description{get;set;}

        public Status Status{get;set;}

        public Priority Priority{get;set;}

        public TimeSpan TimeInterval{get;set;}

        public DateTime StartingTime{get;set;}

        [Required]
        public DateTime OriginalTime{get;set;}
    }
}