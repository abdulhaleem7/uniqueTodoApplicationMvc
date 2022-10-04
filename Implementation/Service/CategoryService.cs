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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse<CategoryDto>> DeleteCategory(int id)
        {
            var category = await _categoryRepository.Get(id);
           if(category == null)
           {
               return new BaseResponse<CategoryDto>
               {
                   Message = "Category not found",
                   Success = false
               };
           }
           category.IsDeleted = true;
           await _categoryRepository.SaveChanges();
            return new BaseResponse<CategoryDto>
            {
                Message = $"Category with name {category.Name} deleted successfully",
                Success = true,
                Data = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategory()
        {
             var category = await _categoryRepository.GetAll(d => d.IsDeleted == false);
            if(category == null)
            {
                return new BaseResponse<IEnumerable<CategoryDto>>
                {
                    Message = "Category doesn't exist",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<CategoryDto>>
            {
                Message = "Category retrieved successfully",
                Success = true,
                Data = category.Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                }).ToList()
            };
        }

        public async Task<BaseResponse<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryRepository.Get(d => d.Id == id && d.IsDeleted == false);
            if(category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                   Message = "Category not found",
                   Success = false
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Message = "Category successfully retrieved",
                Success = true,
                Data = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                }
            };
        }

        public async Task<BaseResponse<CategoryDto>> RegisterCategory(CategoryRequestModel model)
        {
            var category = await _categoryRepository.ExistsByName(model.Name);
           if(category == true)
           {
              return new BaseResponse<CategoryDto>
              {
                  Message = "Category already exist",
                  Success = false
              };
           }
           var categorys = new Category
           {
              Name = model.Name,
              Description = model.Description
           };
           await _categoryRepository.Create(categorys);
           return new BaseResponse<CategoryDto>
           {
               Message = "Category registered successfully",
               Success = true,
               Data = new CategoryDto
               {
                   Name = categorys.Name,
                   Description = categorys.Description
               }
           };
        }

        public async Task<BaseResponse<CategoryDto>> UpdateCategory(int id, UpdateCategoryRequestModel model)
        {
            var category = await _categoryRepository.Get(id);
            if(category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Message = "Category to be updated does not exist",
                    Success = false
                };
            }
            category.Name = model.Name ?? category.Name;
            category.Description = model.Description ?? category.Description;
            await _categoryRepository.Update(category);
            return new BaseResponse<CategoryDto>
            {
                Message = "Category succesfully updated",
                Success = true,
                Data = new CategoryDto
                {
                    Name = category.Name,
                    Description = category.Description,
                }
            };
        }
    }
}