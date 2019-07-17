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
    public class ProjectTemplateController : Controller
    {
        private readonly IProjectTemplateService _projectTemplateService;
        private readonly IReplacePlaceholderService _replacePlaceholderService;
        private readonly ITemplateService _templateService;

        public ProjectTemplateController(IProjectTemplateService projectTemplateService,
                                         ITemplateService templateService,
                                         IReplacePlaceholderService replacePlaceholderService)
        {
            _projectTemplateService = projectTemplateService;
            _templateService = templateService;
            _replacePlaceholderService = replacePlaceholderService;
        }

        // GET: ProjectTemplate
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int templateId, int projectId)
        {
            var content = _projectTemplateService.GetTemplate(templateId, projectId);

            // var replacedContentFromTemplate = _replacePlaceholderService.Replace(template, projectId);
            ViewBag.ProjectId = projectId;
            return PartialView("_Edit", content);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProjectTemplate projectTemplate)
        {
            var updated = _projectTemplateService.CreateOrUpdate(projectTemplate);
            ViewBag.ProjectId = projectTemplate.ProjectId;
            return RedirectToAction("DisplayTemplate", "Requirement", new { projectTemplate.ProjectId});
        }

        [HttpGet]
        public ActionResult ProjectTemplates(int projectId)
        {
            var templates = _templateService.GetTemplates();
            ViewBag.Templates = templates;
            return PartialView("_ProjectTemplates");
        }

        [HttpPost]
        public ActionResult ProjectTemplates(ProjectTemplate projectTemplate)
        {
            var created = _projectTemplateService.CreateOrUpdate(projectTemplate);
            ViewBag.ProjectId = projectTemplate.ProjectId;
            return RedirectToAction("DisplayTemplate", "Requirement", new { projectTemplate.ProjectId });
        }
    }
}