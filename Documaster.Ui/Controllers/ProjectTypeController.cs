using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Documaster.Ui.Models;

namespace Documaster.Ui.Controllers
{
    public class ProjectTypeController : Controller
    {
        private readonly IGenericEntityService<ProjectType> _entityService;
        private readonly IGenericEntityService<Requirement> _requirementService;
        private readonly IGenericEntityService<ProjectTypeRequirement> _projectTypeRequirementService;

        public ProjectTypeController(IGenericEntityService<ProjectType> entityService, IGenericEntityService<Requirement> requirementService, IGenericEntityService<ProjectTypeRequirement> projectTypeRequirementService )
        {
            _entityService = entityService;
            _requirementService = requirementService;
            _projectTypeRequirementService = projectTypeRequirementService;
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
            var reqs = _requirementService.GetAll().Select( x => new AssignedRequirement{Assigned = false, Name = x.Name, Id = x.Id});
            ViewBag.Requirements = reqs;
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProjectTypeViewModel projectType)
        {
            var pt = new ProjectType{Text = projectType.Text, Name = projectType.Name};
            var createdProjectType = _entityService.Create(pt);
            foreach ( var projectTypeRequirementId in projectType.ProjectTypeRequirementIds )
            {
                var ptr = new ProjectTypeRequirement
                {
                    ProjectTypeId = createdProjectType.Id,
                    RequirementId = projectTypeRequirementId
                };
                _projectTypeRequirementService.Create( ptr );
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var projectType = _entityService.Get(id);
            var reqs = _requirementService.GetAll();
            var projectTypeRequirements = _projectTypeRequirementService.Get( x => x.ProjectTypeId == id ).ToList();
            var assignedReqs = new List<AssignedRequirement>();
            foreach ( var req in reqs)
            {
                var assignedReq = new AssignedRequirement
                {
                    Name = req.Name,
                    Id = req.Id,
                    Assigned = projectTypeRequirements.Any( x => x.RequirementId == req.Id)
                };
                assignedReqs.Add(assignedReq);
            }
            var model = new ProjectTypeViewModel
            {
                Text = projectType.Text,
                CreationDate = projectType.CreationDate,
                LastUpdate = projectType.LastUpdate,
                Name = projectType.Name,
                Id = projectType.Id
            };
            ViewBag.Requirements = assignedReqs;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProjectType projectType)
        {
            _entityService.Update(projectType, new List<string> { "Name", "Address" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _entityService.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(ProjectType projectType)
        {
            _entityService.Delete(projectType.Id);
            return RedirectToAction("Index");
        }

    }
}