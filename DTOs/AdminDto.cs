using System;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.DTOs
{
    public class AdminDto
    {
        public int Id{get;set;}

         public string FirstName{get;set;}

        public string LastName{get;set;}

        public string Email{get;set;}

        public User User{get;set;}

        public int UserId{get;set;}

        public DateTime? Modified { get; set; }

        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }

        public string AdminPhoto{get;set;}
    }
}