using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{
    public class SubcategoryController : Controller
    {
        private readonly ISubcategoryService _subcategoryService;
        public SubcategoryController(ISubcategoryService subcategoryService)
        {
            _subcategoryService = subcategoryService;

        }

        public IActionResult Index()
        {
            var subcategory = _subcategoryService.GetAllSubcategory();
            return View(subcategory);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(SubcategoryRequestModel model)
        {
            _subcategoryService.RegisterSubcategory(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var subcategory = _subcategoryService.GetSubcategory(id);
            return View(subcategory);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var subcategory = _subcategoryService.GetSubcategory(id);
            if(subcategory == null)
            {
               return NotFound();
            }
            return View(subcategory);
        }

        [HttpDelete,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _subcategoryService.DeleteSubcategory(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var subcategory = _subcategoryService.GetSubcategory(id);
            if(subcategory == null)
            {
                return NotFound();
            }
            return View(subcategory);
        }

        [HttpPut]
        public IActionResult Update(int id,UpdateSubcategoryRequestModel model)
        {
            _subcategoryService.UpdateSubcategory(id, model);
            return RedirectToAction("Index");
        }
    }
}