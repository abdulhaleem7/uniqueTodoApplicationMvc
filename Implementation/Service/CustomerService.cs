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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        public CustomerService(ICustomerRepository customerRepository, IRoleRepository roleRepository, IUserRepository userRepository, IMailService mailService)
        {
            _mailService = mailService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _customerRepository = customerRepository;

        }

        public async Task<BaseResponse<CustomerDto>> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.Get(id);
           if(customer == null)
           {
               return new BaseResponse<CustomerDto>
               {
                   Message = "Customer not found",
                   Success = false
               };
           }
           customer.IsDeleted = true;
           await _customerRepository.SaveChanges();
           return new BaseResponse<CustomerDto>
           {
               Message = $"Customer with name {customer.FirstName} {customer.LastName} deleted successfully",
               Success = true,
               Data = new CustomerDto
               {
                   Id = customer.Id,
                   FirstName = customer.FirstName,
                   LastName = customer.LastName,
                   Email = customer.Email,
                   CustomerPhoto = customer.CustomerPhoto
               }
           };
        }

        public async Task<BaseResponse<IEnumerable<CustomerDto>>> GetAllCustomer()
        {
            var customer = await _customerRepository.GetAll(a => a.IsDeleted == false);
            if (customer == null)
            {
                return new BaseResponse<IEnumerable<CustomerDto>>
                {
                    Message = "Customers not found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<CustomerDto>>
            {
                Message = "Customer retrieved successfully",
                Success = true,
                Data = customer.Select(customer => new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    CustomerPhoto = customer.CustomerPhoto,
                    MaritalStatus = customer.MaritalStatus
                }).ToList()
            };
        }

        public async Task<BaseResponse<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerRepository.Get(d => d.Id == id && d.IsDeleted == false);
            if (customer == null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Customer doesn't exist",
                    Success = false
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "Customer successfully retrieved",
                Success = true,
                Data = new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    CustomerPhoto = customer.CustomerPhoto,
                    MaritalStatus = customer.MaritalStatus
                }
            };
        }

        public async Task<BaseResponse<CustomerDto>> GetCustomerbymail(string email)
        {
             var customer = await _customerRepository.GetByEmail(email);
            if (customer == null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Customer doesn't exist",
                    Success = false
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "Customer successfully retrieved",
                Success = true,
                Data = new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    CustomerPhoto = customer.CustomerPhoto,
                    MaritalStatus = customer.MaritalStatus
                }
            };
        }

        public async Task<BaseResponse<CustomerDto>> RegisterCustomer(CustomerRequestModel model)
        {
            var custom = await _customerRepository.ExistsByEmail(model.Email);
            if (custom == true)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Customer already exist",
                    Success = false
                };
            }
             var user = new User
            {
               Email = model.Email,
               UserName = $"{model.FirstName} {model.LastName}",
               Password =BCrypt.Net.BCrypt.HashPassword(model.Password)
            };
            var role  = await _roleRepository.GetByName("Customer");
            var userRole = new UserRole
            {
               User = user,
               UserId = user.Id,
               Role = role,
               RoleId = role.Id
            };
            user.UserRoles.Add(userRole);
            var customer = new Customer
            {
               FirstName = model.FirstName,
               LastName = model.LastName,
               Email = model.Email,
               MaritalStatus = model.MaritalStatus,
               CustomerPhoto = model.CustomerPhoto,
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
            var customers = await _customerRepository.Create(customer);
            await _mailService.SendWelcomeEmailAsync(greet);
            return new BaseResponse<CustomerDto>
            {
                Message = "Customer created successfully",
                Success = true,
                Data = new CustomerDto
                {
                    FirstName = customers.FirstName,
                    LastName = customers.LastName,
                    Email = customers.Email,
                    MaritalStatus = customers.MaritalStatus,
                    CustomerPhoto = customers.CustomerPhoto,
                    CreatedBy = $"{customers.FirstName} {customers.LastName}"
                }
            };

        }

        public async Task<BaseResponse<CustomerDto>> UpdateCustomer(int id, UpdateCustomerRequestModel model)
        {
            var customer = await _customerRepository.Get(id);
            if(customer == null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Customer to be updated does not exist",
                    Success = false
                };
            }
            customer.FirstName = model.FirstName ?? customer.FirstName;
            customer.LastName = model.LastName ?? customer.LastName;
            customer.Email = model.Email ?? customer.Email;
            customer.MaritalStatus = model.MaritalStatus;
            customer.CustomerPhoto = model.CustomerPhoto ?? customer.CustomerPhoto;
            await _customerRepository.Update(customer);
            return new BaseResponse<CustomerDto>
            {
                Message = "Customer updated successfully",
                Success = true,
                Data = new CustomerDto
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    MaritalStatus = customer.MaritalStatus,
                    Modified = DateTime.Now,
                    CustomerPhoto = customer.CustomerPhoto,
                    ModifiedBy = $"{customer.FirstName} {customer.LastName}"
                }
            };
        }
    }
}