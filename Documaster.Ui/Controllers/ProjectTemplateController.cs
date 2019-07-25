﻿using Documaster.Business.Services;
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var content = _projectTemplateService.Get(id);

            // var replacedContentFromTemplate = _replacePlaceholderService.Replace(template, projectId);
           // ViewBag.ProjectId = projectId;
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
                return RedirectToAction("CustomerProject", "Requirement", new { projectId });
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult ExportPdf(int id)
        {
            var content = _projectTemplateService.Get(id);

            return new PartialViewAsPdf("_Export", content);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var projectTemplate = _projectTemplateService.Get(id);
            ViewBag.ProjectId = projectTemplate.ProjectId;
            return PartialView("_Delete", projectTemplate);
        }

        [HttpPost]
        public ActionResult Delete(ProjectTemplate projectTemplate)
        {
            var projectTemplateToDelete = _projectTemplateService.Delete(projectTemplate);
           
            return RedirectToAction("CustomerProject", "Requirement", new { projectTemplate.ProjectId});
        }

        public JsonResult DoesNameExist(ProjectTemplate projectTemplate)
        {
            var doesNameExist = _projectTemplateService.DoesNameExist(projectTemplate);
            return Json(!doesNameExist, JsonRequestBehavior.AllowGet);
        }
    }
}