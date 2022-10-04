using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{
    public class TodoitemController : Controller
    {
        private readonly ITodoitemService _todoitemService;
        private readonly ICustomerService _customerservice;

        public TodoitemController(ITodoitemService todoitemService, ICustomerService customerservice)
        {
            _todoitemService = todoitemService;
            _customerservice = customerservice;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTodos()
        {
            var todoitem = await _todoitemService.GetAllTodoitem();
            return View(todoitem.Data);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles="Customer")]
        [HttpPost]
        public  async Task<IActionResult> Create(TodoRequestModel model)
        {
        //     var Mail = User.FindFirst(ClaimTypes.Email).Value;
        //     var rest=  await _customerservice.GetCustomerbymail(Mail);
        //    await _todoitemService.RegisterTodoitem(model, rest.Data.Id);
            var signedInUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var todoitem = await _todoitemService.RegisterTodoitem(model, signedInUserId);
            return RedirectToAction("IndexCustomer", "Customer");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var todoitem = await _todoitemService.GetTodoitem(id);
            return View(todoitem.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var todoitem = await _todoitemService.GetTodoitem(id);
            if (todoitem == null) 
            {
                return ViewBag.Message("Item not found");
            }
            return View(todoitem.Data);
        }

        [HttpDelete,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await _todoitemService.DeleteTodoitem(id);
            return RedirectToAction("GetAllTodos", "Todoitem");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var todoitem = await _todoitemService.GetTodoitem(id);
            if(todoitem == null)
            {
                return ViewBag.Message("Item not found");
            }
            return View(todoitem.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id,UpdateTodoitemRequestModel model)
        {
           await _todoitemService.UpdateTodoitem(id, model);
            return RedirectToAction("GetAllTodos", "Todoitem");
        }
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> Done(int id)
        {
            var res = await _todoitemService.Done(id);
            return RedirectToAction("All");
        }
        [Authorize(Roles = "Customer")]
         [HttpGet]
        public async Task<IActionResult> All()
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllEachCustomerTaskById(rest.Data.Id);
            return View(res.Data);
        }

        public async Task<IActionResult> Today()
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllEachCustomerTodayTaskById(rest.Data.Id);
            return View(res.Data);
        }

        public async Task<IActionResult> TaskDone()
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllEachCustomerDoneTaskById(rest.Data.Id);
            return View(res.Data);
        }

        public async Task<IActionResult> Skipped()
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllEachCustomerSkippedTaskById(rest.Data.Id);
            return View(res.Data);
        }

        public async Task<IActionResult> Upcoming()
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllEachCustomerUpcomingTaskById(rest.Data.Id);
            return View(res.Data);
        }


        [HttpPost]
        public async Task<IActionResult> GetEachCustomerTaskByDate(DateTime OriginalTime)
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllEachCustomerTaskByDate(rest.Data.Id, OriginalTime);
            return View(res.Data);
        }


        [HttpPost]
        public async Task<IActionResult> GetEachCustomerTaskByTime(DateTime OriginalTime)
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllEachCustomerTaskByTime(rest.Data.Id, OriginalTime);
            return View(res.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetEachCustomerTaskByDay(DateTime OriginalTime)
        {
            var rest = await _customerservice.GetCustomerbymail(User.FindFirst(ClaimTypes.Email).Value);
            var res = await _todoitemService.GetAllEachCustomerTaskByDay(rest.Data.Id, OriginalTime);
            return View(res.Data);
        }
    }
}