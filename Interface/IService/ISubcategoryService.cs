using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Interface.IService
{
    public interface ISubcategoryService
    {
        Task<BaseResponse<SubcategoryDto>> RegisterSubcategory(SubcategoryRequestModel model);

        Task<BaseResponse<SubcategoryDto>> UpdateSubcategory(int id,UpdateSubcategoryRequestModel model);

        Task<BaseResponse<SubcategoryDto>> GetSubcategory(int id);

        Task<BaseResponse<IEnumerable<SubcategoryDto>>> GetAllSubcategory();

        Task<BaseResponse<SubcategoryDto>> DeleteSubcategory(int id);
    }
}