using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UniqueTodoApplication.Controllers
{
  // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public AdminController(IAdminService adminService, IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnviroment = webHostEnviroment;
            _adminService = adminService;
        }

        [HttpGet("Admin/IndexAdmin")]
        public IActionResult IndexAdmin()
        {
            return View();
        }

         public async Task<IActionResult> GetAllAdmin()
        {
            var admin = await _adminService.GetAllAdmin();
            return View(admin.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminRequestModel model, IFormFile AdminPhoto)
        {
            string adminPhotoPath = Path.Combine( _webHostEnviroment.WebRootPath, "adminPhotos");
            Directory.CreateDirectory(adminPhotoPath);
            string contentType = AdminPhoto.ContentType.Split('/')[1];
            string adminImage = $"AD{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(adminPhotoPath, adminImage);
            using(var fileStream = new FileStream(fullPath,FileMode.Create))
            {
                AdminPhoto.CopyTo(fileStream);
            }
            model.AdminPhoto = adminImage;
            await _adminService.RegisterAdmin(model);
            return RedirectToAction("SignIn", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var admin = await _adminService.GetAdmin(id);
            return View(admin.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var admin = await _adminService.GetAdmin(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin.Data);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await _adminService.DeleteAdmin(id);
            return RedirectToAction("GetAllAdmin", "Admin");
        }

        public async Task<IActionResult> Profile()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _adminService.GetAdmin(id);
            return View(user.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var admin = await _adminService.GetAdmin(id);
            if (admin == null)
            {
                return ViewBag.Message("User not found");
            }
            return View(admin.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateAdminRequestModel model)
        {
           await _adminService.UpdateAdmin(id, model);
            return RedirectToAction("GetAllAdmin", "Admin");
        }
    }
}