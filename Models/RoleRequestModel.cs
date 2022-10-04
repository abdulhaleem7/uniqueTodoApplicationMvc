using System.ComponentModel.DataAnnotations;

namespace UniqueTodoApplication.Models
{
    public class RoleRequestModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name{get;set;}

        public string Description{get;set;}
    }

    public class UpdateRoleRequestModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name{get;set;}

        public string Description{get;set;}
    }
}