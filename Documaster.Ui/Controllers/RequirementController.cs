using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Documaster.Ui.Models;

namespace Documaster.Ui.Controllers
{
    public class RequirementController : Controller
    {
        private readonly IGenericEntityService<Requirement> _entityService;
        private readonly IGenericEntityService<ProjectRequirement> _projectRequirementService;

        public RequirementController(IGenericEntityService<Requirement> entityService,
            IGenericEntityService<ProjectRequirement> projectRequirementService)
        {
            _entityService = entityService;
            _projectRequirementService = projectRequirementService;
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
            _entityService.Update(requirement, new List<string> { "Name" });
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

        [HttpGet]
        public ActionResult SaveProject(int projectId)
        {
            var model = _entityService.GetAll();
            //var reqs = _entityService.GetAll().Select(x => new AssignedRequirement { Assigned = false, Name = x.Name, Id = x.Id });
            //ViewBag.Requirements = reqs;
            ViewBag.ProjectId = projectId;
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveProject(int projectId,ProjectRequirement requirement)
        {
            var model = _entityService.GetAll();
            var reqs = _entityService.GetAll().Select(x => new AssignedRequirement { Assigned = false, Name = x.Name, Id = x.Id });
            ViewBag.Requirements = reqs;

            _projectRequirementService.Create(requirement);
            return View();
        }

    }
}