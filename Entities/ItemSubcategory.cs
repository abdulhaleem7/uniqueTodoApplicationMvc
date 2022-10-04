namespace UniqueTodoApplication.Entities
{
    public class ItemSubcategory : BaseEntity
    {
        public Item Item{get;set;}

        public int ItemId{get;set;}

        public Subcategory Subcategory{get;set;}

        public int SubcategoryId{get;set;}
    }
}