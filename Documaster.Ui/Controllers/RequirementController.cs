using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Documaster.Ui.Models;

namespace Documaster.Ui.Controllers
{
    public class RequirementController : Controller
    {
        private readonly IGenericEntityService<Requirement> _requirementService;
        private readonly IGenericEntityService<ProjectRequirement> _projectRequirementService;
        private readonly IGenericEntityService<OutputDocument> _outputDocumentService;
        private readonly IGenericEntityService<Category> _categoryService;

        public RequirementController(IGenericEntityService<Requirement> entityService,
            IGenericEntityService<ProjectRequirement> projectRequirementService,
            IGenericEntityService<OutputDocument> outputDocumentService,
            IGenericEntityService<Category> categoryService)
        {
            _requirementService = entityService;
            _projectRequirementService = projectRequirementService;
            _outputDocumentService = outputDocumentService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _requirementService.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //TODO: load list of all categories
            //send them via viewbag

            var categoryList = _categoryService.GetAll();
            ViewBag.Categories = categoryList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Requirement requirement)
        {
            _requirementService.Create(requirement);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //TODO: load list of all categories
            //send them via viewbag
            var categoryList = _categoryService.GetAll();
            ViewBag.Categories = categoryList;

            var model = _requirementService.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Requirement requirement)
        {
            _requirementService.Update(requirement, new List<string> { "Name", "CategoryId" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _requirementService.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Requirement requirement)
        {
            _requirementService.Delete(requirement.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ProjectRequirements(int projectId)
        {
            var requirements = _requirementService.GetAll();

            var assisgnedProjectRequirements = _projectRequirementService
                .GetAll().Where(x => x.ProjectId == projectId);

            var categories = _categoryService.GetAll().OrderBy(x => x.Name)
                             .Select(x => new AssignedCategory
                             {
                                 Id = x.Id,
                                 Name = x.Name,
                                 AssignedRequirements = x.Requirements.Select(y => new AssignedRequirement
                                 {
                                     Assigned = y.ProjectRequirements.Any(z => z.ProjectId == projectId),
                                     Name = y.Name,
                                     Id = y.Id
                                 }).ToList()
                             });

            ViewBag.ProjectId = projectId;
            return View("ProjectRequirements", categories);
        }

        [HttpPost]
        public ActionResult SaveProjectRequirements(int projectId, IEnumerable<int> assignedRequirements)
        {
            var dbProjectRequirements = _projectRequirementService.Get(x => x.ProjectId == projectId).ToList();
            var deletedProjectRequirements = dbProjectRequirements
                .Where(x => assignedRequirements == null || !assignedRequirements.Any(y => y == x.RequirementId)).ToList();

            foreach (var item in deletedProjectRequirements)
            {
                _projectRequirementService.Delete(item.Id);
            }

            var newProjectRequirements = assignedRequirements?.Where(x => !dbProjectRequirements.Any(y => y.RequirementId == x))
                ?? new List<int>();

            foreach (var item in newProjectRequirements)
            {
                var projectRequirement = new ProjectRequirement
                {
                    RequirementId = item,
                    ProjectId = projectId
                };
                _projectRequirementService.Create(projectRequirement);

            }
            return RedirectToAction("Index", "Project");
        }

        [HttpGet]
        public ActionResult CustomerProject(int projectId)
        {

            var model2 = _requirementService.GetAll().Where(x => x.ProjectRequirements.Any(y => y.ProjectId == projectId));




            List<FileToUpdate> fileToUpdates = new List<FileToUpdate>();



            foreach (var item in model2)
            {
                var file = item.OutputDocuments.FirstOrDefault();

                var newFileToUpdate = new FileToUpdate
                {
                    Id = file?.Id ?? 0,
                    FileName = file?.Name,
                    ProjectId = projectId,
                    RequirementName = item.Name,
                    Status = !string.IsNullOrEmpty(file?.Name),
                    RequirementId = item.Id
                };

                fileToUpdates.Add(newFileToUpdate);
            }


            fileToUpdates = fileToUpdates.OrderByDescending(x => x.Status).ThenBy(x => x.RequirementName).ToList();

            return View(fileToUpdates);
        }

        //Metoda pentru incarcarea fisierelor 
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase fileUpload, int projectId, int requirementId)
        {
            if (fileUpload == null)
            {
                return null;
            }
            else
            {
                var length = fileUpload.ContentLength;
                byte[] tempImage = new byte[length];
                fileUpload.InputStream.Read(tempImage, 0, length);



                var output = new OutputDocument
                {

                    Name = fileUpload.FileName,
                    DocumentData = tempImage,
                    ContentType = fileUpload.ContentType,
                    ProjectId = projectId,
                    RequirementId = requirementId

                };
                var created = _outputDocumentService.Create(output);
            }

            var outputDocuments = _outputDocumentService.GetAll().Where(x => x.ProjectId == projectId);
            ViewBag.OutputDocuments = outputDocuments;

            return RedirectToAction("CustomerProject", new { projectId = projectId });
        }

        public ActionResult DeleteDocument(int documentId, int projectId)
        {
            _outputDocumentService.Delete(documentId);


            return RedirectToAction("CustomerProject", new { projectId = projectId });
        }

        public ActionResult DownloadDocument(int documentId)
        {
            var document = _outputDocumentService.Get(documentId);


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
    }
}