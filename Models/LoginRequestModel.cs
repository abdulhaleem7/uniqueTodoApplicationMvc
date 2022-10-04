using System.ComponentModel.DataAnnotations;
using UniqueTodoApplication.DTOs;

namespace UniqueTodoApplication.Models
{
    public class LoginRequestModel
    {
        [Required]
        public string UserNameOrEmail{get;set;}

        [Required]
        public string Password{get;set;}
    }

    public class LoginResponseDto : BaseResponse<UserDto>
    {
        public string Token{get;set;}
    }
}