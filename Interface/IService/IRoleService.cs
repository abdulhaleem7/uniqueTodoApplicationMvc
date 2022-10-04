using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Interface.IService
{
    public interface IRoleService
    {
        Task<BaseResponse<RoleDto>> RegisterRole(RoleRequestModel model);

        Task<BaseResponse<RoleDto>> UpdateRole(int id,UpdateRoleRequestModel model);

        Task<BaseResponse<RoleDto>> GetRole(int id);

        Task<BaseResponse<IEnumerable<RoleDto>>> GetAllRole();

        Task<BaseResponse<RoleDto>> DeleteRole(int id);
    }
}