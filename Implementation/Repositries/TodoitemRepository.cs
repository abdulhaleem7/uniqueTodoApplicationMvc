using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniqueTodoApplication.Context;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Interface.IRepositries;

namespace UniqueTodoApplication.Implementation.Repositries
{
    public class TodoitemRepository : BaseRepository<Todoitem>, ITodoitemRepository
    {
        public TodoitemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Todoitem> Done(int Id)
        {
            return await _context.Todoitems.FirstOrDefaultAsync(n => n.Id == Id && n.IsDeleted == false);
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.Todoitems.AnyAsync(t => t.Id == id && t.IsDeleted == false);
        }

        public async Task<Todoitem> GetByName(string name)
        {
           return await _context.Todoitems.FirstOrDefaultAsync(n => n.Name.Equals(name) && n.IsDeleted == false);
        }

        public async Task<bool> ExistsByNameAndTime(string name, DateTime time)
        {
            return await _context.Todoitems.AnyAsync(d => d.Name.Equals(name) && d.OriginalTime == time && d.IsDeleted == false);
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllEachCustomerTaskById(int customerId)
        {
           return await _context.Todoitems
           .Where(a => a.CustomerId == customerId && a.IsDeleted == false)
           .Select(c => new TodoitemDto
           {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority

           }).ToListAsync();
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllEachCustomerTodayTaskById(int customerId)
        {
            return await _context.Todoitems.Where(a => a.CustomerId == customerId && a.Status == Enum.Status.Today && a.IsDeleted == false)
            .Select(c => new TodoitemDto
            {
                Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority
            }).ToListAsync();
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllEachCustomerDoneTaskById(int customerId)
        {
            return await _context.Todoitems.Where(a => a.CustomerId == customerId && a.Status == Enum.Status.TaskDone && a.IsDeleted == false)
            .Select(c => new TodoitemDto
            {
                 Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority
            }).ToListAsync();
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllEachCustomerTaskByDate(int customerId, DateTime date)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.Date == date.Date && a.CustomerId == customerId && a.IsDeleted == false)
            .Select(c => new TodoitemDto
            {
                 Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority
            }).ToListAsync();
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllEachCustomerTaskByTime(int customerId, DateTime time)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.Hour == time.Hour && a.CustomerId == customerId && a.IsDeleted == false)
            .Select(c => new TodoitemDto
            {
                  Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority
            }).ToListAsync();
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllEachCustomerTaskByDay(int customerId, DateTime day)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.DayOfWeek == day.DayOfWeek && a.CustomerId == customerId && a.IsDeleted == false)
            .Select(c => new TodoitemDto
            {
                 Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority
            }).ToListAsync();
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllTaskByDate(DateTime date)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.Date == date.Date && a.IsDeleted == false)
            .Select(c => new TodoitemDto
            {
                 Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority
            }).ToListAsync();
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllTaskByTime(DateTime time)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.ToShortTimeString() == time.ToShortTimeString() && a.IsDeleted == false)
            .Select(c => new TodoitemDto
            {
                 Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority
            }).ToListAsync();
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllTaskByDay(DateTime day)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.DayOfWeek == day.DayOfWeek && a.IsDeleted == false)
            .Select(c => new TodoitemDto
            {
                Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority
            }).ToListAsync();
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllEachCustomerSkippedTaskId(int customerId)
        {
            return await _context.Todoitems.Where(a => a.CustomerId == customerId && a.Status == Enum.Status.Skipped && a.IsDeleted == false)
            .Select(c => new TodoitemDto
            {
                 Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority
            }).ToListAsync();
        }

        public async Task<IEnumerable<TodoitemDto>> GetAllEachCustomerUpcomingTaskId(int customerId)
        {
            return await _context.Todoitems.Where(a => a.CustomerId == customerId && a.Status == Enum.Status.UpComing && a.IsDeleted == false)
            .Select(c => new TodoitemDto
            {
                 Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OriginalTime = c.OriginalTime,
                    StartingTime = c.StartingTime,
                    Status = c.Status,
                    TimeInterval = c.TimeInterval,
                    Priority = c.Priority
            }).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllSkippedTodoitem()
        {
            return await _context.Todoitems.Where(d => d.Status == Enum.Status.Skipped && d.IsDeleted == false).ToListAsync();
            
        }

        public async Task<IEnumerable<Todoitem>> GetAllExpiredTime()
        {
           return await _context.Todoitems.Where(d => d.OriginalTime > DateTime.Now).ToListAsync();
        }

        public async Task<bool> Update(IList<Todoitem> todo)
        {
            _context.Todoitems.UpdateRange(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}