using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;

namespace Documaster.Ui.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _categoryService.GetCategories();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            _categoryService.CreateCategory(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _categoryService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            _categoryService.EditCategory(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _categoryService.DeleteById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            _categoryService.DeleteCategory(category);
            return RedirectToAction("Index");
        }
    }
}