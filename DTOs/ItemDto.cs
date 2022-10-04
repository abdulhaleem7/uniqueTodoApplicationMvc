using System;
using System.Collections.Generic;

namespace UniqueTodoApplication.DTOs
{
    public class ItemDto
    {
        public int Id{get;set;}

         public string Name{get;set;}

        public string Description{get;set;}

        public DateTime? Modified { get; set; }

        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }

        public List<SubcategoryDto> Category{get;set;} = new List<SubcategoryDto>();
    }
}