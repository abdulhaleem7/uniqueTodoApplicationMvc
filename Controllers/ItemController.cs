using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;

        }

        public IActionResult Index()
        {
            var item = _itemService.GetAllItem();
            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(ItemRequestModel model)
        {
            _itemService.RegisterItem(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var item = _itemService.GetItem(id);
            return View(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = _itemService.GetItem(id);
            if(item == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpDelete,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _itemService.DeleteItem(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var item = _itemService.GetItem(id);
            if(item == null)
            {
                return NotFound();
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update(int id,UpdateItemRequestModel model)
        {
            _itemService.UpdateItem(id, model);
            return RedirectToAction("Index");
        }
    }
}