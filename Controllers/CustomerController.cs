using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{
    
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CustomerController(ICustomerService customerService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

            _customerService = customerService;
        }

        [HttpGet("Customer/IndexCustomer")]
        public IActionResult IndexCustomer()
        {
            return View();
        }

        public async Task<IActionResult> GetAllCustomer()
        {
            var customer = await _customerService.GetAllCustomer();
            return View(customer.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequestModel model, IFormFile customerPhoto)
        {
            string customerPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, "customerPhotos");
            Directory.CreateDirectory(customerPhotoPath);
            string contentType = customerPhoto.ContentType.Split('/')[1];
            string customerImage = $"CTM{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(customerPhotoPath, customerImage);
            using(var fileStream = new FileStream(fullPath,FileMode.Create))
            {
                customerPhoto.CopyTo(fileStream);
            }
            model.CustomerPhoto = customerImage;
            await _customerService.RegisterCustomer(model);
            return RedirectToAction("SignIn", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            return View(customer.Data);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Profile()
        {
            //int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var customer = User.FindFirst(ClaimTypes.Email).Value;
            var custom = await _customerService.GetCustomerbymail(customer);
            var user = await _customerService.GetCustomer(custom.Data.Id);
            return View(user.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer.Data);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          await  _customerService.DeleteCustomer(id);
            return RedirectToAction("GetAllCustomer", "Customer");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
            {
                return ViewBag.Message("User not found");
            }
            return View(customer.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateCustomerRequestModel model)
        {
          await  _customerService.UpdateCustomer(id, model);
            return RedirectToAction("GetAllCustomer", "Customer");
        }
    }
}