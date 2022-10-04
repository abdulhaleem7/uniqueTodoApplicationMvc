using System.Collections.Generic;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Entities
{
    public class User : BaseEntity
    {
        public string Email{get;set;}

        public string Password{get;set;}

        public string UserName{get;set;}

        public Admin Admin{get;set;}

        public Customer Customer{get;set;}

        public List<UserRole> UserRoles{get;set;} = new List<UserRole>();
    }
}