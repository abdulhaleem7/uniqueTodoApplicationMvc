using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Interface.IRepositries;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Implementation.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public async Task<BaseResponse<UserDto>> DeleteUser(int id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User not found",
                    Success = false
                };
            }
            user.IsDeleted = true;
            await _userRepository.SaveChanges();
            return new BaseResponse<UserDto>
            {
                Message = $"User with mail {user.Email} deleted successfully",
                Success = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>> GetAllUser()
        {
            var user = await _userRepository.GetAll(d => d.IsDeleted == false);
            if(user == null)
            {
                return new BaseResponse<IEnumerable<UserDto>>
                {
                    Message = "Users not found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<UserDto>>
            {
                Message = "User found",
                Success = true,
                Data = user.Select(user => new UserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password
                }).ToList()
            };
        }

        public async Task<BaseResponse<UserDto>> GetUser(int id)
        {
             var user = await _userRepository.Get(id);
            if(user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User not found",
                    Success = false
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "User found",
                Success = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password
                }
            };
        }

        public async Task<BaseResponse<UserDto>> Login(LoginRequestModel model)
        {
            //var user = await _userRepository.Get(e => (e.Email == model.UserNameOrEmail || e.UserName == model.UserNameOrEmail) && model.Password == e.Password);
            var user = await _userRepository.GetByEmail(model.UserNameOrEmail);
            if (user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "Invalid Username or password",
                    Success = false,
                };
            }

            var userVerify = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            if(userVerify == false)
            {
                return new BaseResponse<UserDto>
                {
                    Success = false,
                    Message = "Invalid Username or password"
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "User successfully found",
                Success = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(user => new RoleDto
                    {
                        Name = user.Role.Name
                    }).ToList()
                }
            };
        
        }
    }
}