using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Documaster.Business.Models;

namespace Documaster.Ui.Controllers
{
    public class RequirementController : Controller
    {
        private readonly IRequirementService _requirementService;
        private readonly ICategoryService _categoryService;
        private readonly IProjectRequirementService _projectRequirementService;
        private readonly IOutputDocumentService _outputDocumentService;
        private readonly IProjectService _projectService;
        private readonly INamedEntityService<Requirement> _namedEntityService;
        private readonly ICustomizeTabService _customizeTabService;
        private readonly ITemplateService _templateService;
        private readonly IProjectTemplateService _projectTemplateService;
        private readonly IReplacePlaceholderService _replacePlaceholderService;

        public RequirementController(IRequirementService requirementService,
                                     ICategoryService categoryService,
                                     IProjectRequirementService projectRequirementService,
                                     IOutputDocumentService outputDocumentService,
                                     IProjectService projectService,
                                     ICustomizeTabService customizeTabService,
                                     INamedEntityService<Requirement> namedEntityService,
                                     ITemplateService templateService,
                                     IProjectTemplateService projectTemplateService,
                                     IReplacePlaceholderService replacePlaceholderService)
        {
            _requirementService = requirementService;
            _categoryService = categoryService;
            _projectRequirementService = projectRequirementService;
            _outputDocumentService = outputDocumentService;
            _projectService = projectService;
            _namedEntityService = namedEntityService;
           _customizeTabService = customizeTabService;
            _templateService = templateService;
            _projectTemplateService = projectTemplateService;
            _replacePlaceholderService = replacePlaceholderService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _requirementService.GetRequirements().OrderBy(x => x.Category.Number).ThenBy(x => x.Number);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categoryList = _categoryService.GetListOfCategories();
            ViewBag.Categories = categoryList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Requirement requirement)
        {
            if (ModelState.IsValid)
            {
                _requirementService.CreateRequirement(requirement);
                return RedirectToAction("Index");
            }
            return View(requirement);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var categoryList = _categoryService.GetListOfCategories();
            ViewBag.Categories = categoryList;

            var model = _requirementService.GetRequirementById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Requirement requirement)
        {
            _requirementService.UpdateRequirement(requirement);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _requirementService.GetRequirementById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Requirement requirement)
        {
            var isDeleted = _requirementService.DeleteRequirement(requirement);
            TempData["DeleteSuccess"] = isDeleted;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ProjectRequirements(int projectId)
        {
            var categories = _categoryService.GetCategoriesByAssignedCategory(projectId);
            ViewBag.ProjectId = projectId;
            return View("ProjectRequirements", categories);
        }

        [HttpPost]
        public ActionResult SaveProjectRequirements(int projectId, IEnumerable<int> assignedRequirements)
        {
            var dbProjectRequirements = _projectRequirementService.GetListOfProjectRequirements(projectId);
            var deletedProjectRequirements = dbProjectRequirements
                .Where(x => assignedRequirements == null || assignedRequirements.All(y => y != x.RequirementId)).ToList();

            foreach (var item in deletedProjectRequirements)
            {
                _projectRequirementService.DeleteProjectRequirement(item);
            }

            var newRequirementIds = assignedRequirements?.Where(x => dbProjectRequirements.All(y => y.RequirementId != x))
                ?? new List<int>();

            foreach (var requirementId in newRequirementIds)
            {
                var projectRequirement = new Model.Entities.ProjectRequirement
                {
                    RequirementId = requirementId,
                    ProjectId = projectId
                };
                _projectRequirementService.CreateProjectRequirement(projectRequirement);

            }
            _projectRequirementService.Save();

            return RedirectToAction("Index", "Project");
        }

        public ActionResult ProjectCategory(int projectId)
        {
            var categories = _categoryService.GetCategoriesByAssignedCategory(projectId);
            ViewBag.Categories = categories;
            return View("CustomerProject", categories);
        }

        [HttpGet]
        public ActionResult CustomerProject(int projectId)
        {
            var project = _projectService.Get(projectId);

            var customizeTabs = _customizeTabService.GetCustomizeTabs().OrderBy(x => x.Number).ToList();

            ViewBag.CustomizeTabs = customizeTabs;

            ViewBag.ProjectId = projectId;
            return View(project);
        }

        [HttpGet]
        public ActionResult OutputDocuments(int projectId, int customizeTabId)
        {
            var fileToUpdates = new List<FileToUpdate>();

            var projectRequirements = _projectRequirementService.GetProjectRequirementByProjectId(projectId);
            foreach (var projectRequirement in projectRequirements)
            {
               
                var outputDocument = _outputDocumentService.GetOutputDocuments(projectId, projectRequirement.RequirementId, customizeTabId); 
                var newFileToUpdate = new FileToUpdate
                {
                    Id = outputDocument?.Id ?? 0,
                    FileName = outputDocument?.Name,
                    ProjectId = projectId,
                    RequirementName = projectRequirement.Requirement.Name,
                    CategoryNumber = projectRequirement.Requirement.Category.Number,
                    CategoryName = projectRequirement.Requirement.Category.Name,
                    RequirementNumber = projectRequirement.Requirement.Number,
                    IsReady = projectRequirement.IsReady,
                    RequirementId = projectRequirement.RequirementId,
                    ProjectRequirementId = projectRequirement.Id
                };

                fileToUpdates.Add(newFileToUpdate);
            }
            fileToUpdates = fileToUpdates.OrderBy(x => x.CategoryNumber).ThenBy(x => x.RequirementNumber).ToList();
            ViewBag.ProjectId = projectId;
            ViewBag.CustomizeTabId = customizeTabId;
            return PartialView("_ProjectDocumentForRequirement", fileToUpdates);
        }

        [HttpGet]
        public ActionResult Notes(int projectId)
        {
            var project = _projectService.Get(projectId);
            if (project == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ProjectNotes", project);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Notes(int id, string notes)
        {
            var project = _projectService.Get(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            project.Notes = notes;
            _projectService.UpdateNotes(project);
            return RedirectToAction("CustomerProject", new { projectId = id });
        }

        [HttpPost]
        public ActionResult ToggleDocumentState(int projectRequirementId, bool state)
        {
            var projectRequirement = _projectRequirementService.GetProjectRequirementById(projectRequirementId);
            if (projectRequirement == null)
                return HttpNotFound();

            projectRequirement.IsReady = state;
            _projectRequirementService.UpdateProjectRequirement(projectRequirement);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        //Metoda pentru incarcarea fisierelor
        [HttpPost]
        public void Upload(HttpPostedFileBase fileUpload, int projectId, int? requirementId,int customizeTabId)
        {
            _outputDocumentService.CreateOutputDocument(fileUpload, projectId, requirementId, customizeTabId);
            var outputDocuments = _outputDocumentService.GetOutputDocumentByProjectId(projectId);
            ViewBag.OutputDocuments = outputDocuments;
        }

        public void DeleteDocument(int documentId)
        {
            _outputDocumentService.DeleteOutputDocument(documentId);
        }

        [HttpGet]
        public ActionResult DownloadDocument(int documentId)
        {
            var document = _outputDocumentService.GetOutputDocumentById(documentId);
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = document.Name,
                // always prompt the user for downloading, set to true if you want
                // the browser to try to show the file inline
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(document.DocumentData, document.ContentType);
        }

        [HttpGet]
        public ActionResult DisplayDocuments(int projectId, int customizeTabId)
        {
            var documentType = _customizeTabService.GetCustomizeTabById(customizeTabId).Type;
            var model = _outputDocumentService.GetOutputDocumentByIdAndDocType(projectId, customizeTabId, documentType);
            ViewBag.ProjectId = projectId;
            ViewBag.CustomizeTabId = customizeTabId;
            ViewBag.DocumentType = documentType;
            return PartialView("_ProjectDocument", model);
        }

        [HttpGet]
        public PartialViewResult PreviewDocument(int documentId)
        {
            var document = _outputDocumentService.GetOutputDocumentById(documentId);
            return PartialView("_PreviewDocument", document);
        }

        public JsonResult DoesCategoryNumberCombinationExist(Requirement requirement)
        {
            var doesCategoryNumberCombinationExist = _requirementService.DoesCategoryNumberCombinationExist(requirement);
            return Json(!doesCategoryNumberCombinationExist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DoesNameExist(Requirement requirement)
        {
            var doesNameExist = _namedEntityService.DoesNameExist(requirement);
            return Json(!doesNameExist, JsonRequestBehavior.AllowGet);
        }

        /*
         * Display list of projectTemplates
         */
        [HttpGet]
        public ActionResult DisplayTemplate(int projectId)
        {
            var projectTemplates = _projectTemplateService.GetProjectTemplates(projectId);
            ViewBag.ProjectId = projectId;
            return PartialView("_DisplayTemplates", projectTemplates);
        }


    }
}