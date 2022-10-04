using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Interface.IRepositries;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.MailFolder;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Implementation.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;

        public AdminService(IAdminRepository adminRepository,IRoleRepository roleRepository, IUserRepository userRepository, IMailService mailService)
        {
            _adminRepository = adminRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _mailService = mailService;
        }
        public async Task<BaseResponse<AdminDto>> DeleteAdmin(int id)
        {
            var admin = await _adminRepository.Get(id);
            if(admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Message = "Admin not found",
                    Success = false
                };
            }
            admin.IsDeleted = true;
           await _adminRepository.SaveChanges();
           return new BaseResponse<AdminDto>
           {
               Message = $"Admin with name {admin.FirstName} {admin.LastName} deleted successfully",
               Success = true,
               Data = new AdminDto
               {
                   Id = admin.Id,
                   FirstName = admin.FirstName,
                   LastName = admin.LastName,
                   Email = admin.Email,
                   AdminPhoto = admin.AdminPhoto
               }
           };
        }

        public async Task<BaseResponse<AdminDto>> GetAdmin(int id)
        {
            var admin = await _adminRepository.Get(d => d.Id == id && d.IsDeleted == false);
           if(admin == null)
           {
               return new BaseResponse<AdminDto>
               {
                   Message = "No admin found",
                   Success = false
               };
           }
           return new BaseResponse<AdminDto>
           {
               Message = "Admin successfully retrieved",
               Success = true,
               Data = new AdminDto
               {
                   Id = admin.Id,
                   FirstName = admin.FirstName,
                   LastName = admin.LastName,
                   Email = admin.Email,
                   AdminPhoto = admin.AdminPhoto
               }
           };
        }

        public async Task<BaseResponse<IEnumerable<AdminDto>>> GetAllAdmin()
        {
            var admin = await _adminRepository.GetAll(d => d.IsDeleted == false);
           if(admin == null)
           {
               return new BaseResponse<IEnumerable<AdminDto>>
               {
                  Message = "Admin does not exist",
                  Success = false
               };
           }
           return new BaseResponse<IEnumerable<AdminDto>>
           {
               Message = "Admin successfully retrieved",
               Success = true,
               Data = admin.Select(admin => new AdminDto
               {
                   Id = admin.Id,
                   FirstName = admin.FirstName,
                   LastName = admin.LastName,
                   Email = admin.Email,
                   AdminPhoto = admin.AdminPhoto
                  
               }).ToList()
           };
        }

        public async Task<BaseResponse<AdminDto>> RegisterAdmin(AdminRequestModel model)
        {
            var admin = await _adminRepository.ExistsByEmail(model.Email);
            if(admin == true)
            {
                return new BaseResponse<AdminDto>
                {
                    Message = "Admin already exist",
                    Success = false
                };
            }
            var user = new User
            {
               Email = model.Email,
               UserName = $"{model.FirstName} {model.LastName}",
               Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };
            var role  = await _roleRepository.GetByName("Admin");
            var userRole = new UserRole
            {
               User = user,
               UserId = user.Id,
               Role = role,
               RoleId = role.Id
            };
            user.UserRoles.Add(userRole);
            var admins = new Admin
            {
               FirstName = model.FirstName,
               LastName = model.LastName,
               Email = model.Email,
               AdminPhoto = model.AdminPhoto,
               User = user,
               UserId = user.Id
            };
            var greet = new WelcomeRequest
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
            await _userRepository.Create(user);
            var adminss = await _adminRepository.Create(admins);
            await _mailService.SendWelcomeEmailAsync(greet);
            return new BaseResponse<AdminDto>
            {
               Message = "Admin created successfully",
               Success = true,
               Data = new AdminDto
               {
                  FirstName = adminss.FirstName,
                  LastName = adminss.LastName,
                  Email = adminss.Email,
                  AdminPhoto = adminss.AdminPhoto,
                  CreatedBy = $"{adminss.FirstName} {adminss.LastName}"
                }
            };
        }

        public async Task<BaseResponse<AdminDto>> UpdateAdmin(int id, UpdateAdminRequestModel model)
        {
            var admin = await _adminRepository.Get(id);
            if(admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Message = "Admin doesn't exist",
                    Success = false
                };
            }
            admin.FirstName = model.FirstName ?? admin.FirstName;
            admin.LastName = model.LastName ?? admin.LastName;
            admin.Email = model.Email ?? admin.LastName;
            admin.AdminPhoto = model.AdminPhoto ?? admin.AdminPhoto;
            await _adminRepository.Update(admin);
            return new BaseResponse<AdminDto>
            {
                Message = "Admin updated successfully",
                Success = true,
                Data = new AdminDto
                {
                   FirstName = admin.FirstName,
                   LastName = admin.LastName,
                   Email = admin.Email,
                   Modified = DateTime.Now,
                   AdminPhoto = admin.AdminPhoto,
                   ModifiedBy = $"{admin.FirstName} {admin.LastName}"
                }
            };
        }
    }
}