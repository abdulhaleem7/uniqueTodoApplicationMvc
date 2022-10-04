using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Interface.IService
{
    public interface ICategoryService
    {
        Task<BaseResponse<CategoryDto>> RegisterCategory(CategoryRequestModel model);

        Task<BaseResponse<CategoryDto>> UpdateCategory(int id,UpdateCategoryRequestModel model);

        Task<BaseResponse<CategoryDto>> GetCategory(int id);

        Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategory();

        Task<BaseResponse<CategoryDto>> DeleteCategory(int id);
    }
}