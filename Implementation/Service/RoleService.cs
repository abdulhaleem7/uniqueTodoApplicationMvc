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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

        }

        public async Task<BaseResponse<RoleDto>> DeleteRole(int id)
        {
            var role = await _roleRepository.Get(id);
            if(role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Role not found",
                    Success = false
                };
            }
            role.IsDeleted = true;
            await _roleRepository.SaveChanges();
            return new BaseResponse<RoleDto>
            {
                Message = $"Role with name {role.Name} deleted successfully",
                Success = true,
                Data = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<RoleDto>>> GetAllRole()
        {
            var role = await _roleRepository.GetAll(d => d.IsDeleted == false);
            if(role == null)
            {
                return new BaseResponse<IEnumerable<RoleDto>>
                {
                    Message = "Roles not found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<RoleDto>>
            {
                Message = "Role retrieved successfully",
                Success = true,
                Data = role.Select(role => new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description
                }).ToList()
            };
        }

        public async Task<BaseResponse<RoleDto>> GetRole(int id)
        {
            var role = await _roleRepository.Get(d => d.Id == id && d.IsDeleted == false);
           if(role == null)
           {
               return new BaseResponse<RoleDto>
               {
                   Message = "Role does not exist",
                   Success = false
               };
           }
           return new BaseResponse<RoleDto>
           {
               Message = "Role retrieved successfully",
               Success = true,
               Data = new RoleDto
               {
                   Id = role.Id,
                   Name = role.Name,
                   Description = role.Description
               }
           };
        }

        public async Task<BaseResponse<RoleDto>> RegisterRole(RoleRequestModel model)
        {
            var role = await _roleRepository.ExistsByName(model.Name);
            if(role == true)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Role already exist",
                    Success = false
                };
            }
            var roles = new Role
            {
                Name = model.Name,
                Description = model.Description
            };
            await _roleRepository.Create(roles);
            return new BaseResponse<RoleDto>
            {
                Message = "Role successfully created",
                Success = true,
                Data = new RoleDto
                {
                    Name = roles.Name,
                    Description = roles.Description
                }
            };
        }

        public async Task<BaseResponse<RoleDto>> UpdateRole(int id, UpdateRoleRequestModel model)
        {
            var role = await _roleRepository.Get(id);
            if(role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Role does not exist",
                    Success = false
                };
            }
            role.Name = model.Name ?? role.Name;
            role.Description = model.Description ?? role.Description;
            await _roleRepository.Update(role);
            return new BaseResponse<RoleDto>
            {
                Message = "Role updated successfully",
                Success = true,
                Data = new RoleDto
                {
                    Name = role.Name,
                    Description = role.Description
                }
            };
        }
    }
}