using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;

namespace Documaster.Ui.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly INamedEntityService<Category> _namedEntityService;

        public CategoryController(ICategoryService categoryService,
                                  INamedEntityService<Category> namedEntityService)
        {
            _categoryService = categoryService;
            _namedEntityService = namedEntityService;
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
            if (ModelState.IsValid)
            {
                _categoryService.CreateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
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

        public JsonResult DoesNumberExist(int number)
        {
            var doesNumberExist = _categoryService.GetCategoryByNumber(number);
             if(doesNumberExist != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DoesNameExist(string name)
        {
            var doesNameExist = _namedEntityService.DoesNameExist(name);
            return Json(!doesNameExist, JsonRequestBehavior.AllowGet);
        }
    }
}