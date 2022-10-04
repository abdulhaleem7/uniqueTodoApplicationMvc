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
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        public SubcategoryService(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;

        }

        public async Task<BaseResponse<SubcategoryDto>> DeleteSubcategory(int id)
        {
            var subcategory = await _subcategoryRepository.Get(id);
            if(subcategory == null)
            {
                return new BaseResponse<SubcategoryDto>
                {
                    Message = "Subcategory not found",
                    Success = false
                };
            }
            subcategory.IsDeleted = true;
            await _subcategoryRepository.SaveChanges();
            return new BaseResponse<SubcategoryDto>
            {
                Message = $"Subcategory ith name {subcategory.Name} deleted successfully",
                Success = true,
                Data = new SubcategoryDto
                {
                    Id = subcategory.Id,
                    Name = subcategory.Name,
                    Description = subcategory.Description
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<SubcategoryDto>>> GetAllSubcategory()
        {
            var subcategory = await _subcategoryRepository.GetAll(t => t.IsDeleted == false);
            if(subcategory == null)
            {
                return new BaseResponse<IEnumerable<SubcategoryDto>>
                {
                    Message = "Subcategory not found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<SubcategoryDto>>
                {
                    Message = "Subcategory successfully retrieved",
                    Success = true,
                    Data = subcategory.Select(subcategory => new SubcategoryDto
                    {
                        Id = subcategory.Id,
                        Name = subcategory.Name,
                        Description = subcategory.Description
                    }).ToList()
                }; 
        }

        public async Task<BaseResponse<SubcategoryDto>> GetSubcategory(int id)
        {
            var subcategory = await _subcategoryRepository.Get(b => b.Id == id && b.IsDeleted == false);
            if(subcategory == null)
            {
                return new BaseResponse<SubcategoryDto>
                {
                    Message = "Subategory does not exist",
                    Success = false
                };
            }
            return new BaseResponse<SubcategoryDto>
            {
                Message = "Subcategory retrieved succesfully",
                Success = true,
                Data = new SubcategoryDto
                {
                    Id = subcategory.Id,
                    Name = subcategory.Name,
                    Description = subcategory.Description
                }
            };
        }

        public async Task<BaseResponse<SubcategoryDto>> RegisterSubcategory(SubcategoryRequestModel model)
        {
            var subcategory = await _subcategoryRepository.ExistsByName(model.Name);
            if(subcategory == true)
            {
               return new BaseResponse<SubcategoryDto>
               {
                   Message = "Subcategory already exist",
                   Success = false,
               };
            }
            var subcategorys = new Subcategory
            {
               Name = model.Name,
               Description = model.Description
            };
            await _subcategoryRepository.Create(subcategorys);
            return new BaseResponse<SubcategoryDto>
            {
                Message = "Subcategory created successfully",
                Success = true,
                Data = new SubcategoryDto
                {
                    Id = subcategorys.Id,
                    Name = subcategorys.Name,
                    Description = subcategorys.Description
                }
            };
        }

        public async Task<BaseResponse<SubcategoryDto>> UpdateSubcategory(int id, UpdateSubcategoryRequestModel model)
        {
            var subcategory = await _subcategoryRepository.Get(id);
            if(subcategory == null)
            {
                return new BaseResponse<SubcategoryDto>
                {
                    Message = "Subcategory doesn't exist",
                    Success = false
                };
            }
            subcategory.Name = model.Name;
            subcategory.Description = model.Description;
            await _subcategoryRepository.Update(subcategory);
            return new BaseResponse<SubcategoryDto>
            {
                Message = "Subcategory uypdated successfully",
                Success = true,
                Data = new SubcategoryDto
                {
                    Id = subcategory.Id,
                    Name = subcategory.Name,
                    Description = subcategory.Description
                }
            };
        }
    }
}