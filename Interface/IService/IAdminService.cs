using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Interface.IService
{
    public interface IAdminService
    {
        Task<BaseResponse<AdminDto>> RegisterAdmin(AdminRequestModel model);

        Task<BaseResponse<AdminDto>> UpdateAdmin(int id,UpdateAdminRequestModel model);  

        Task<BaseResponse<AdminDto>> GetAdmin(int id);

        Task<BaseResponse<IEnumerable<AdminDto>>> GetAllAdmin();

        Task<BaseResponse<AdminDto>> DeleteAdmin(int id);
    }
}