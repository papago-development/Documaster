using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;

namespace Documaster.Ui.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = unitOfWork.Repository<Category>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _categoryRepository.GetAll().OrderBy(x => x.Name).ToList();
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
            _categoryRepository.Create(category);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _categoryRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            _categoryRepository.Update(category, new List<string> { "Name" });
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _categoryRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            _categoryRepository.Delete(category.Id);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}