using Documaster.Business.Services;
using Documaster.Model.Entities;
using Documaster.Ui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Documaster.Ui.Controllers
{
    public class ProjectRequirementController : Controller
    {
        private readonly IGenericEntityService<ProjectRequirement> _projectRequirementService;
        private readonly IGenericEntityService<Project> _entityService;

        public ProjectRequirementController(IGenericEntityService<ProjectRequirement> projectRequirementService,
            IGenericEntityService<Project> entityService)
        {
            _projectRequirementService = projectRequirementService;
            _entityService = entityService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _projectRequirementService.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult CustomerProject(int projectId)
        {
            //  var model = _projectRequirementService.GetAll().Where(p=>p.ProjectId==projectId);
            var model = _projectRequirementService.GetAll();
            ViewBag.ProjectId = projectId;
            return View(model);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    var reqs = _projectRequirementService.GetAll().Select(x => new AssignedRequirement { Assigned = false, Name = x.Name, Id = x.Id });
        //    ViewBag.Requirements = reqs;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(ProjectTypeViewModel projectType)
        //{
        //    var pt = new ProjectType { Text = projectType.Text, Name = projectType.Name };
        //    var createdProjectType = _entityService.Create(pt);
        //    foreach (var projectTypeRequirementId in projectType.ProjectTypeRequirementIds)
        //    {
        //        var ptr = new ProjectTypeRequirement
        //        {
        //            ProjectTypeId = createdProjectType.Id,
        //            RequirementId = projectTypeRequirementId
        //        };
        //        _projectTypeRequirementService.Create(ptr);
        //    }

        //    return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var projectType = _entityService.Get(id);
        //    var reqs = _requirementService.GetAll();
        //    var projectTypeRequirements = _projectTypeRequirementService.Get(x => x.ProjectTypeId == id).ToList();
        //    var assignedReqs = new List<AssignedRequirement>();
        //    foreach (var req in reqs)
        //    {
        //        var assignedReq = new AssignedRequirement
        //        {
        //            Name = req.Name,
        //            Id = req.Id,
        //            Assigned = projectTypeRequirements.Any(x => x.RequirementId == req.Id)
        //        };
        //        assignedReqs.Add(assignedReq);
        //    }
        //    var model = new ProjectTypeViewModel
        //    {
        //        Text = projectType.Text,
        //        CreationDate = projectType.CreationDate,
        //        LastUpdate = projectType.LastUpdate,
        //        Name = projectType.Name,
        //        Id = projectType.Id
        //    };
        //    ViewBag.Requirements = assignedReqs;
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Edit(ProjectType projectType)
        //{
        //    _entityService.Update(projectType, new List<string> { "Name", "Address" });
        //    return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    var model = _entityService.Get(id);
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Delete(ProjectType projectType)
        //{
        //    _entityService.Delete(projectType.Id);
        //    return RedirectToAction("Index");
        //}


    }
}