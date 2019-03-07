using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Documaster.Model.Enums;
using Documaster.Business.Models;

namespace Documaster.Ui.Controllers
{
    public class RequirementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<OutputDocument> _outputDocumentRepository;
        private readonly IRequirementService _requirementService;
        private readonly ICategoryService _categoryService;
        private readonly IProjectRequirementService _projectRequirementService;
        private readonly IOutputDocumentService _outputDocumentService;
        private readonly IProjectService _projectService;

        public RequirementController(IRequirementService requirementService,
                                     ICategoryService categoryService,
                                     IProjectRequirementService projectRequirementService,
                                     IOutputDocumentService outputDocumentService,
                                     IProjectService projectService,
                                     IUnitOfWork unitOfWork,
                                     IGenericRepository<OutputDocument> outputDocumentRepository)
        {
            _unitOfWork = unitOfWork;
            _outputDocumentRepository = outputDocumentRepository;

            _requirementService = requirementService;
            _categoryService = categoryService;
            _projectRequirementService = projectRequirementService;
            _outputDocumentService = outputDocumentService;
            _projectService = projectService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _requirementService.GetRequirements().OrderBy(x=>x.Category.Number).ThenBy(x =>x.Number);
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
            _requirementService.CreateRequirement(requirement);
            return RedirectToAction("Index");
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
            _requirementService.DeleteRequirement(requirement);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ProjectRequirements(int projectId)
        {
            //Ordonare dupa categorii
            // ???
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
            //var categories = _categoryRepository
            //          .Get(x => x.Requirements.Any())
            //          .Select(x => new AssignedCategory
            //          {
            //              Id = x.Id,
            //              Name = x.Name,
            //              AssignedRequirements = x.Requirements.Select(y => new AssignedRequirement
            //              {
            //                  Assigned = y.ProjectRequirements.Any(z => z.ProjectId == projectId),
            //                  Name = y.Name,
            //                  Id = y.Id
            //              }).ToList()
            //          });

            var categories = _categoryService.GetCategoriesByAssignedCategory(projectId);
            ViewBag.Categories = categories;
            return View("CustomerProject", categories);
        }

        [HttpGet]
        public ActionResult CustomerProject(int projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
        }

        [HttpGet]
        public ActionResult OutputDocuments(int projectId)
        {
            var fileToUpdates = new List<FileToUpdate>();

            // ???
            var projectRequirements = _projectRequirementService.GetProjectRequirementByProjectId(projectId);
            foreach (var projectRequirement in projectRequirements)
            {
                var outputDocument = _outputDocumentService.GetOutputDocuments(projectId, projectRequirement.Id);
                var newFileToUpdate = new FileToUpdate
                {
                    Id = outputDocument?.Id ?? 0,
                    FileName = outputDocument?.Name,
                    ProjectId = projectId,
                    RequirementName = projectRequirement.Requirement.Name,
                    IsReady = projectRequirement.IsReady,
                    RequirementId = projectRequirement.RequirementId,
                    ProjectRequirementId = projectRequirement.Id
                };

                fileToUpdates.Add(newFileToUpdate);
            }
            fileToUpdates = fileToUpdates.OrderBy(x => x.RequirementName).ToList();
            ViewBag.ProjectId = projectId;
            return PartialView("_ProjectDocumentForRequirement", fileToUpdates);
        }

        [HttpGet]
        public ActionResult Notes(int projectId)
        {
            var project = _projectService.GetProjectByProjectId(projectId);
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
            var project = _projectService.GetProjectByNoteId(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            project.Notes = notes;
            _projectService.UpdateProject(project);
              return RedirectToAction("CustomerProject", new {projectId = id});
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
        public void Upload(HttpPostedFileBase fileUpload, int projectId, int? requirementId, string documentType)
        {
            if (fileUpload == null || !Enum.TryParse<DocumentType>(documentType, true, out var parsedDocumentType))
            {
                return;
            }
            var length = fileUpload.ContentLength;
            var tempImage = new byte[length];
            fileUpload.InputStream.Read(tempImage, 0, length);

            var output = new OutputDocument
            {
                Name = fileUpload.FileName,
                DocumentData = tempImage,
                ContentType = fileUpload.ContentType,
                DocumentType = parsedDocumentType.ToString(),
                ProjectId = projectId,
                RequirementId = requirementId,
            };
            _outputDocumentRepository.Create(output);
            _unitOfWork.SaveChanges();
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
        public ActionResult DisplayDocuments(int projectId, string documentType)
        {
            if (!Enum.TryParse<DocumentType>(documentType, true, out var parsedDocumentType))
            {
                return HttpNotFound();
            }

            var model = _outputDocumentService.GetOutputDocumentByIdAndDocType(projectId, documentType);
            ViewBag.ProjectId = projectId;
            ViewBag.DocumentType = parsedDocumentType.ToString();

            return PartialView("_ProjectDocument", model);
        }

        [HttpGet]
        public PartialViewResult PreviewDocument(int documentId)
        {
            var document = _outputDocumentService.GetOutputDocumentById(documentId);
            return PartialView("_PreviewDocument", document);
        }
    }
}
