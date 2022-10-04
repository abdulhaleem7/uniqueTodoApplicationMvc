using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Interface.IRepositries;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Implementation.Service
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        public ItemService(IItemRepository itemRepository, ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
            _itemRepository = itemRepository;

        }

        public async Task<BaseResponse<ItemDto>> DeleteItem(int id)
        {
            var item = await _itemRepository.Get(id);
            if(item == null)
            {
                return new BaseResponse<ItemDto>
                {
                    Message = "Item not found",
                    Success = false
                };
            }
            item.IsDeleted = true;
            await _itemRepository.SaveChanges();
            return new BaseResponse<ItemDto>
            {
                Message = $"Item with name {item.Name} deleted successfully",
                Success = true,
                Data = new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<ItemDto>>> GetAllItem()
        {
            var item = await _itemRepository.GetAll(r => r.IsDeleted == false);
            if(item == null)
            {
                return new BaseResponse<IEnumerable<ItemDto>>
                {
                    Message = "Items does not exist",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<ItemDto>>
            {
                Message = "Item retrieved successfully",
                Success = true,
                Data = item.Select(item => new ItemDto
                {
                    Id = item.Id,
                   Name = item.Name,
                   Description = item.Description
                }).ToList()
            };
        }

        public async Task<BaseResponse<ItemDto>> GetItem(int id)
        {
            var item = await _itemRepository.Get(d => d.Id == id && d.IsDeleted == false);
            if(item == null)
            {
                return new BaseResponse<ItemDto>
                {
                    Message = "Item not found",
                    Success = false
                };
            }
            return new BaseResponse<ItemDto>
            {
                Message = "Item retrieved successfully",
                Success = true,
                Data = new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description
                }
            };
        }

        public BaseResponse<IEnumerable<ItemDto>> GetItemBySubcategoryId(int subcategoryId)
        {
            var item =  _itemRepository.GetItemBySubcategory(subcategoryId);
            if(item == null)
            {
                return new BaseResponse<IEnumerable<ItemDto>>
                {
                    Message = "Item not found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<ItemDto>>
            {
                Message = "Item retrieved successfully",
                Success = true,
                Data = item.Select(item => new ItemDto
                {
                   Name = item.Name,
                   Description = item.Description
                }).ToList()
            };
        }

        public async Task<BaseResponse<ItemDto>> RegisterItem(ItemRequestModel model)
        {
            var item = await _itemRepository.ExistsByName(model.Name);
            if(item == true)
            {
                return new BaseResponse<ItemDto>
                {
                    Message = "Item already exist",
                    Success = false
                };
            }
            var items = new Item
            {
               Name = model.Name,
               Description = model.Description
            };
            var subCategories = _subcategoryRepository.GetSelected(model.Subcategorys);
            foreach(var subcategory in subCategories)
            {
                var itemSubcategory = new ItemSubcategory
                {
                    Item = items,
                    ItemId = items.Id
                };
                items.ItemSubcategories.Add(itemSubcategory);
            }
           await _itemRepository.Create(items);
           return new BaseResponse<ItemDto>
           {
               Message = "Item created successfully",
               Success = true,
               Data = new ItemDto
               {
                   Name = items.Name,
                   Description = items.Description
               }
           };
        }

        public async Task<BaseResponse<ItemDto>> UpdateItem(int id, UpdateItemRequestModel model)
        {
            var item = await _itemRepository.Get(id);
            if(item == null)
            {
                return new BaseResponse<ItemDto>
                {
                    Message = "Item to be updated doesn't exist",
                    Success = false
                };
            }
            item.Name = model.Name ?? item.Name;
            item.Description = model.Description ?? item.Description;
            await _itemRepository.Update(item);
            return new BaseResponse<ItemDto>
            {
                Message = "Item updated successfully",
                Success = true,
                Data = new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description
                }
            };

        }
    }
}