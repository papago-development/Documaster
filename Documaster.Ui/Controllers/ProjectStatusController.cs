using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;

namespace Documaster.Ui.Controllers
{
    public class ProjectStatusController : Controller
    {

        private readonly IProjectStatusService _projectStatusService;

        public ProjectStatusController(IProjectStatusService projectStatusService)
        {
            _projectStatusService = projectStatusService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            var statuses = _projectStatusService.GetAll();
            return View (statuses);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProjectStatus projectStatus)
        {
            _projectStatusService.CreateProjectStatus(projectStatus);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _projectStatusService.GetProjectStatusById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(ProjectStatus projectStatus)
        {
            _projectStatusService.DeleteProjectStatus(projectStatus);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id) 
        {
            var model = _projectStatusService.GetProjectStatusById(id);
            return View(model);
         }

        [HttpPost]
        public ActionResult Edit(ProjectStatus projectStatus)
        {
            _projectStatusService.EditProjectStatus(projectStatus);
            return RedirectToAction("Index");
        }
    }
}
