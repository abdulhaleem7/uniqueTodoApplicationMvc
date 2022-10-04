using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;

        }

        public async Task<IActionResult> GetAllRole()
        {
            var role = await _roleService.GetAllRole();
            return View(role.Data);
        }
 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleRequestModel model)
        {
            await _roleService.RegisterRole(model);
            return RedirectToAction("IndexAdmin", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetRole(id);
            return View(role.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetRole(id);
            if(role == null)
            {
                return ViewBag.Message("Role not found");
            }
            return View(role.Data);
        }

        [HttpDelete,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await _roleService.DeleteRole(id);
            return RedirectToAction("GetAllRole", "Role");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var role = await _roleService.GetRole(id);
            if(role == null)
            {
                return ViewBag.Message("Role not found");
            }
            return View(role.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id,UpdateRoleRequestModel model)
        {
           await _roleService.UpdateRole(id, model);
            return RedirectToAction("GetAllRole", "Role");
        }
    }
}