using System.ComponentModel.DataAnnotations;
using UniqueTodoApplication.Enum;

namespace UniqueTodoApplication.Models
{
    public class CustomerRequestModel
    {
        [Required]
        [StringLength(maximumLength:30,MinimumLength = 7)]
        public string FirstName{get;set;}

        [Required]
        [StringLength(maximumLength:30,MinimumLength = 7)]
        public string LastName{get;set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email{get;set;}

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 7)]
        public string Password{get;set;}

        [Required]
        [Compare("Password",ErrorMessage = "Password and the confirm password must be the same")]        
        public string ConfirmPassword{get;set;}
         
        public MaritalStatus MaritalStatus{get;set;}

        public string CustomerPhoto{get;set;}
    }

    public class UpdateCustomerRequestModel
    {
        [Required]
        [StringLength(maximumLength:30,MinimumLength = 7)]
        public string FirstName{get;set;}

        [Required]
        [StringLength(maximumLength:30,MinimumLength = 7)]
        public string LastName{get;set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email{get;set;}

        public MaritalStatus MaritalStatus{get;set;}

        public string CustomerPhoto{get;set;}
    }
}