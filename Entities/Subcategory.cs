using System.Collections.Generic;

namespace UniqueTodoApplication.Entities
{
    public class Subcategory : BaseEntity
    {
        public string Name{get;set;}

        public string Description{get;set;}

        public int CategoryId{get;set;}

        public List<ItemSubcategory> ItemSubcategories{get;set;} = new List<ItemSubcategory>();
    }
}