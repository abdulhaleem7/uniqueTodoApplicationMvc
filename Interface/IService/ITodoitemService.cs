using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Interface.IService
{
    public interface ITodoitemService
    {
        
        Task<BaseResponse<TodoitemDto>> RegisterTodoitem(TodoRequestModel model,int id);

        Task<BaseResponse<TodoitemDto>> UpdateTodoitem(int id,UpdateTodoitemRequestModel model);

        Task<BaseResponse<TodoitemDto>> GetTodoitem(int id);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTodoitem();

        Task<BaseResponse<TodoitemDto>> DeleteTodoitem(int id);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTodayTask();

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllUpcomingTask();

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllDoneTask();

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllSkippedTask();

       Task<BaseResponse<IEnumerable<TodoitemDto>>> FetchAllCustomerTaskForToday();

       Task<BaseResponse<TodoitemDto>> GetTodoitemByCustomerId(int id);

       Task<BaseResponse<TodoitemDto>> Done(int id);

       Task<BaseResponse<TodoitemDto>> Skipped(int id);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllEachCustomerTaskById(int customerId);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllEachCustomerTodayTaskById(int customerId);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllEachCustomerDoneTaskById(int customerId);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllEachCustomerSkippedTaskById(int customerId);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllEachCustomerUpcomingTaskById(int customerId);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllEachCustomerTaskByDate(int customerId, DateTime date);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllEachCustomerTaskByTime(int customerId, DateTime time);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllEachCustomerTaskByDay(int customerId, DateTime day);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTaskByDate(DateTime date);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTaskByTime(DateTime time);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTaskByDay(DateTime day);

       Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllSkippedTodoitem(); 

       Task<BaseResponse<IEnumerable<Todoitem>>> GetAllExpiredTime();

       Task<BaseResponse<bool>> UpdateTodoitemToSkipped(IList<Todoitem> todoitem);
    }
}