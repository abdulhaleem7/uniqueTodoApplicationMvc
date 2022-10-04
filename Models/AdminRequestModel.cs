using System.ComponentModel.DataAnnotations;

namespace UniqueTodoApplication.Models
{
    public class AdminRequestModel
    {
        [Required]
        [MinLength(7)]
        [MaxLength(30)]
        public string FirstName{get;set;}

        [Required]
        [MinLength(7)]
        [MaxLength(30)]
        public string LastName{get;set;}

        [Required]
        [RegularExpression("@ .com")]
        [DataType(DataType.EmailAddress)]
        public string Email{get;set;}

        [Required]
        [MinLength(7)]
        [MaxLength(15)]
        public string Password{get;set;}

        [Required]
        [Compare("Password",ErrorMessage = "Password and the confirm password must be the same")]
        public string ConfirmPassword{get;set;}

        public string AdminPhoto{get;set;}
    }

    public class UpdateAdminRequestModel
    {

        [Required]
        [MinLength(7)]
        [MaxLength(30)]
        public string FirstName{get;set;}

        [Required]
        [MinLength(7)]
        [MaxLength(30)]
        public string LastName{get;set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email{get;set;}

        public string AdminPhoto{get;set;}
    }
}