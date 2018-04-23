using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Documaster.Ui.Models;

namespace Documaster.Ui.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenericEntityService<Category> _categoryService;
        private readonly IGenericEntityService<ProjectRequirement> _projectRequirementService;
        private readonly IGenericEntityService<OutputDocument> _outputDocumentService;

        public CategoryController(IGenericEntityService<Category> categoryService)
        {
            _categoryService = categoryService;
          
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _categoryService.GetAll();
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
            _categoryService.Create(category);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _categoryService.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            _categoryService.Update(category, new List<string> { "Name" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _categoryService.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            _categoryService.Delete(category.Id);
            return RedirectToAction("Index");
        }
    }
}