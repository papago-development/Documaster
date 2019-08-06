using Documaster.Business.Services;
using Documaster.Model.Entities;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Documaster.Ui.Controllers
{
    [Authorize]
    public class ProjectTemplateController : Controller
    {
        private readonly IProjectTemplateService _projectTemplateService;
        private readonly IReplacePlaceholderService _replacePlaceholderService;
        private readonly ITemplateService _templateService;

        public ProjectTemplateController(IProjectTemplateService projectTemplateService, ITemplateService templateService, 
             IReplacePlaceholderService replacePlaceholderService)
        {
            _projectTemplateService = projectTemplateService;
            _templateService = templateService;
            _replacePlaceholderService = replacePlaceholderService;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var content = _projectTemplateService.Get(id);

            return PartialView("_Edit", content);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProjectTemplate projectTemplate)
        {
            var updated = _projectTemplateService.Update(projectTemplate);
            ViewBag.ProjectId = projectTemplate.ProjectId;
            return RedirectToAction("DisplayTemplate", "Requirement", new { projectTemplate.ProjectId});
        }

        [HttpGet]
        public ActionResult Create(int projectId)
        {
            var templates = _templateService.GetTemplates();
            ViewBag.Templates = templates;
            ViewBag.ProjectId = projectId;
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(int projectId, int templateId, string name)
        {
            if(ModelState.IsValid)
            {
                var created = _projectTemplateService.Create(projectId, templateId, name);
                ViewBag.ProjectId = projectId;
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult ExportPdf(int id)
        {
            var content = _projectTemplateService.Get(id);

            return new PartialViewAsPdf("_Export", content);
        }

        public void Delete(int id)
        {
            _projectTemplateService.Delete(id);
        }

        public JsonResult DoesNameExist(ProjectTemplate projectTemplate)
        {
            var doesNameExist = _projectTemplateService.DoesNameExist(projectTemplate);
            return Json(!doesNameExist, JsonRequestBehavior.AllowGet);
        }
    }
}