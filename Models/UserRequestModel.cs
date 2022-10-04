using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Models
{
    public class UserRequestModel
    {
        public string Email{get;set;}

        public string Password{get;set;}

        public string UserName{get;set;}

        public Admin Admin{get;set;}

        public Customer Customer{get;set;}
    }

    public class UpdateUserRequestModel
    {

         public string Email{get;set;}

        public string Password{get;set;}

        public string UserName{get;set;}

        public Admin Admin{get;set;}

        public Customer Customer{get;set;}
    }
}