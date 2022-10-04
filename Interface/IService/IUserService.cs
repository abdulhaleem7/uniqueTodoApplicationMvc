using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Interface.IService
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> Login(LoginRequestModel model);

        Task<BaseResponse<IEnumerable<UserDto>>> GetAllUser();

        Task<BaseResponse<UserDto>> GetUser(int id);

        Task<BaseResponse<UserDto>> DeleteUser(int id);
    }
}