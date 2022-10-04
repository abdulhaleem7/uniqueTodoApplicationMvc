using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Interface.IService
{
    public interface ICustomerService
    {
         Task<BaseResponse<CustomerDto>> RegisterCustomer(CustomerRequestModel model);

         Task<BaseResponse<CustomerDto>> UpdateCustomer(int id,UpdateCustomerRequestModel model);

         Task<BaseResponse<CustomerDto>> GetCustomer(int id);
         Task<BaseResponse<CustomerDto>> GetCustomerbymail(string email);

         Task<BaseResponse<IEnumerable<CustomerDto>>> GetAllCustomer();

        Task<BaseResponse<CustomerDto>> DeleteCustomer(int id);


    }
}