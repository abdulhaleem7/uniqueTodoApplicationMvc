using System.ComponentModel.DataAnnotations;

namespace UniqueTodoApplication.Models
{
    public class CategoryRequestModel
    {
        [Required]
        [StringLength(maximumLength:20,MinimumLength = 7)]
        public string Name{get;set;}

        public string Description{get;set;}
    }

    public class UpdateCategoryRequestModel
    {
        [Required]
        [StringLength(maximumLength:20,MinimumLength = 7)]
        public string Name{get;set;}

        public string Description{get;set;}
    }
}