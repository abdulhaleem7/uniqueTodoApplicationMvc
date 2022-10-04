using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }

        public async Task<IActionResult> Index()
        {
            var category = await _categoryService.GetAllCategory();
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequestModel model)
        {
           await _categoryService.RegisterCategory(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategory(id);
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategory(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          await  _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetCategory(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int id,UpdateCategoryRequestModel model)
        {
           await _categoryService.UpdateCategory(id, model);
            return RedirectToAction("Index");
        }
    }
}