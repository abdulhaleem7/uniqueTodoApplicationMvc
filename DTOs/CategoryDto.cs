using System;
using System.Collections.Generic;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.DTOs
{
    public class CategoryDto
    {
        public int Id{get;set;}

        public string Name{get;set;}

        public string Description{get;set;}

        public DateTime? Modified { get; set; }

        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }

        public List<Subcategory> Subcategories{get;set;} = new List<Subcategory>();
    }
}