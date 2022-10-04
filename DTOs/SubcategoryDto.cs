using System;
using System.Collections.Generic;

namespace UniqueTodoApplication.DTOs
{
    public class SubcategoryDto
    {
        public int Id{get;set;}
        
        public string Name{get;set;}

        public string Description{get;set;}

        public int CategoryId{get;set;}

        public DateTime? Modified { get; set; }

        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }

        public List<ItemDto> Item{get;set;} = new List<ItemDto>();
    }
}