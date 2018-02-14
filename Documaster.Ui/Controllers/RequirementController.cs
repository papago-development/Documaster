using System.Collections.Generic;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;

namespace Documaster.Ui.Controllers
{
    public class RequirementController : Controller
    {
        private readonly IGenericEntityService<Requirement> _entityService;

        public RequirementController(IGenericEntityService<Requirement> entityService)
        {
            _entityService = entityService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _entityService.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Requirement requirement)
        {
            _entityService.Create(requirement);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _entityService.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Requirement requirement)
        {
            _entityService.Update(requirement, new List<string> { "Name", "Address" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _entityService.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Requirement requirement)
        {
            _entityService.Delete(requirement.Id);
            return RedirectToAction("Index");
        }

    }
}