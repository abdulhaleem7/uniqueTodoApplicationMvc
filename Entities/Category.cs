using System.Collections.Generic;

namespace UniqueTodoApplication.Entities
{
    public class Category : BaseEntity
    {
        public string Name{get;set;}

        public string Description{get;set;}

        public List<Subcategory> SubCategories{get;set;} = new List<Subcategory>();
    }
}