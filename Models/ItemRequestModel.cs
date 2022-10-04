using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniqueTodoApplication.Models
{
    public class ItemRequestModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 7)]
        public string Name{get;set;}

        public string Description{get;set;}

        public IList<int> Subcategorys { get; set; } = new List<int>();
    }

    public class UpdateItemRequestModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 7)]
        public string Name{get;set;}

        public string Description{get;set;}
    }
}