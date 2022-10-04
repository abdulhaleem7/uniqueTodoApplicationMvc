using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Interface.IService
{
    public interface IItemService
    {
         Task<BaseResponse<ItemDto>> RegisterItem(ItemRequestModel model);

        Task<BaseResponse<ItemDto>> UpdateItem(int id,UpdateItemRequestModel model);

        Task<BaseResponse<ItemDto>> GetItem(int id);

        Task<BaseResponse<IEnumerable<ItemDto>>> GetAllItem();

        Task<BaseResponse<ItemDto>> DeleteItem(int id);

        BaseResponse<IEnumerable<ItemDto>> GetItemBySubcategoryId(int subcategoryId);
    }
}