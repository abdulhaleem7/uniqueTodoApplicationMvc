using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Entities;


namespace UniqueTodoApplication.Interface.IRepositries
{
    public interface ITodoitemRepository : IRepository<Todoitem>
    {
        Task<bool> ExistsByNameAndTime(string name, DateTime time);

        Task<bool> ExistsById(int id);

        Task<Todoitem> GetByName(string name);

        Task<Todoitem> Done(int Id);

        Task<IEnumerable<Todoitem>> GetAllSkippedTodoitem();

        Task<IEnumerable<TodoitemDto>> GetAllEachCustomerTaskById(int customerId);

        Task<IEnumerable<TodoitemDto>> GetAllEachCustomerTodayTaskById(int customerId);

       Task<IEnumerable<TodoitemDto>> GetAllEachCustomerDoneTaskById(int customerId);

       Task<IEnumerable<TodoitemDto>> GetAllEachCustomerTaskByDate(int customerId, DateTime date);

       Task<IEnumerable<TodoitemDto>> GetAllEachCustomerTaskByTime(int customerId, DateTime time);

       Task<IEnumerable<TodoitemDto>> GetAllEachCustomerTaskByDay(int customerId, DateTime day);

       Task<IEnumerable<TodoitemDto>> GetAllTaskByDate(DateTime date);

       Task<IEnumerable<TodoitemDto>> GetAllTaskByTime(DateTime time);

       Task<IEnumerable<TodoitemDto>> GetAllTaskByDay(DateTime day);

       Task<IEnumerable<TodoitemDto>> GetAllEachCustomerSkippedTaskId(int customerId);

       Task<IEnumerable<TodoitemDto>> GetAllEachCustomerUpcomingTaskId(int customerId);

       Task<IEnumerable<Todoitem>> GetAllExpiredTime();

       Task<bool> Update(IList<Todoitem> todo);
    }
}