namespace UniqueTodoApplication.Entities
{
    public class Admin : BaseEntity
    {
        public string FirstName{get;set;}

        public string LastName{get;set;}

        public string Email{get;set;}

        public User User{get;set;}

        public int UserId{get;set;}

        public string AdminPhoto{get;set;}
    }
}