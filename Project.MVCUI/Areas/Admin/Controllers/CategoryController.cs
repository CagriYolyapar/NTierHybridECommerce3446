using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Managers.Abstracts;
using Project.BLL.Managers.Concretes;
using Project.ENTITIES.Models;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {

        readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager  )
        {
            _categoryManager = categoryManager;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _categoryManager.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            await _categoryManager.CreateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _categoryManager.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category model)
        {
            await _categoryManager.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Pacify(int id)
        {
            await _categoryManager.MakePassiveAsync(await _categoryManager.GetByIdAsync(id));
            return RedirectToAction("Index");
        }
    }
}
