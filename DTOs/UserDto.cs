using System;
using System.Collections.Generic;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.DTOs
{
    public class UserDto
    {
         public int Id{get;set;}

         public string Email{get;set;}

        public string Password{get;set;}

        public string UserName{get;set;}

        public Admin Admin{get;set;}

        public Customer Customer{get;set;}

        public DateTime? Modified { get; set; }

        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }

        public List<RoleDto> Roles{get;set;} = new List<RoleDto>();
    }
}