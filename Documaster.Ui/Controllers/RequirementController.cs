using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Documaster.Ui.Models;

namespace Documaster.Ui.Controllers
{
    public class RequirementController : Controller
    {
        private readonly IGenericRepository<Requirement> _requirementRepository;
        private readonly IGenericRepository<ProjectRequirement> _projectRequirementRepository;
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IGenericRepository<OutputDocument> _outputDocumentRepository;
        private readonly IGenericRepository<ProjectDocument> _projectDocumentRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequirementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _requirementRepository = unitOfWork.Repository<Requirement>();
            _projectRequirementRepository = unitOfWork.Repository<ProjectRequirement>();
            _outputDocumentRepository = unitOfWork.Repository<OutputDocument>();
            _categoryRepository = unitOfWork.Repository<Category>();
            _projectRepository = unitOfWork.Repository<Project>();
            _projectDocumentRepository = unitOfWork.Repository<ProjectDocument>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _requirementRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categoryList = _categoryRepository.GetAll();
            ViewBag.Categories = categoryList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Requirement requirement)
        {
            _requirementRepository.Create(requirement);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var categoryList = _categoryRepository.GetAll();
            ViewBag.Categories = categoryList;

            var model = _requirementRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Requirement requirement)
        {
            _requirementRepository.Update(requirement, new List<string> { "Name", "CategoryId" });
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _requirementRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Requirement requirement)
        {
            _requirementRepository.Delete(requirement.Id);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ProjectRequirements(int projectId)
        {
            var categories = _categoryRepository
                             .Get(x => x.Requirements.Any())
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
            var dbProjectRequirements = _projectRequirementRepository.Get(x => x.ProjectId == projectId).ToList();
            var deletedProjectRequirements = dbProjectRequirements
                .Where(x => assignedRequirements == null || assignedRequirements.All(y => y != x.RequirementId)).ToList();

            foreach (var item in deletedProjectRequirements)
            {
                _projectRequirementRepository.Delete(item.Id);
            }

            var newRequirementIds = assignedRequirements?.Where(x => dbProjectRequirements.All(y => y.RequirementId != x))
                ?? new List<int>();

            foreach (var requirementId in newRequirementIds)
            {
                var projectRequirement = new ProjectRequirement
                {
                    RequirementId = requirementId,
                    ProjectId = projectId
                };
                _projectRequirementRepository.Create(projectRequirement);

            }
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index", "Project");
        }

        public ActionResult ProjectCategory(int projectId)
        {
            var categories = _categoryRepository
                      .Get(x => x.Requirements.Any())
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
            var model2 = _requirementRepository.GetAll().Where(x => x.ProjectRequirements.Any(y => y.ProjectId == projectId));
            var fileToUpdates = new List<FileToUpdate>();

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
                    RequirementId = item.Id,

                };

                fileToUpdates.Add(newFileToUpdate);
            }

            fileToUpdates = fileToUpdates.OrderByDescending(x => x.Status).ThenBy(x => x.RequirementName).ToList();

            return PartialView("_CustomerProject", fileToUpdates);
        }

        //Metoda pentru incarcarea fisierelor
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase fileUpload, int projectId, int requirementId)
        {
            if (fileUpload == null)
            {
                return null;
            }

            var length = fileUpload.ContentLength;
            var tempImage = new byte[length];
            fileUpload.InputStream.Read(tempImage, 0, length);

            var output = new OutputDocument
            {

                Name = fileUpload.FileName,
                DocumentData = tempImage,
                ContentType = fileUpload.ContentType,
                ProjectId = projectId,
                RequirementId = requirementId

            };
            _outputDocumentRepository.Create(output);
            _unitOfWork.SaveChanges();

            var outputDocuments = _outputDocumentRepository.GetAll().Where(x => x.ProjectId == projectId);
            ViewBag.OutputDocuments = outputDocuments;

            return RedirectToAction("OutputDocuments", new { projectId });
        }

        public ActionResult DeleteDocument(int documentId, int projectId)
        {
            _outputDocumentRepository.Delete(documentId);
            _unitOfWork.SaveChanges();

            return RedirectToAction("OutputDocuments", new { projectId });
        }

        [HttpGet]
        public ActionResult DownloadDocument(int documentId)
        {
            var document = _outputDocumentRepository.Get(documentId);
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


        // Afisare fotografii
        [HttpGet]
        public ActionResult Photos(int projectId)
        {
            var projectDocuments = _projectRepository.GetAll().Where(x => x.ProjectDocuments.Any(y => y.ProjectId == projectId));
            var photos = new List<PhotoToUpdate>();

            foreach (var item in projectDocuments)
            {
                var photo = item.ProjectDocuments.FirstOrDefault();

                var newPhoto = new PhotoToUpdate
                {
                    Id = photo?.Id ?? 0,
                    FileName = photo?.Name,
                    ProjectId = projectId,

                };
                photos.Add(newPhoto);
            }
            photos = photos.ToList();
            return PartialView("_ProjectDocument", photos);
        }

        [HttpPost]
        public ActionResult UploadPhoto(HttpPostedFileBase fileUpload, int projectId)
        {
            if (fileUpload == null)
            {
                return null;
            }

            var length = fileUpload.ContentLength;
            var tempImage = new byte[length];
            fileUpload.InputStream.Read(tempImage, 0, length);

            var photo = new ProjectDocument
            {

                Name = fileUpload.FileName,
                DocumentData = tempImage,
                //   ContentType = fileUpload.ContentType,
                ProjectId = projectId,


            };
            _projectDocumentRepository.Create(photo);
            _unitOfWork.SaveChanges();

            var projectDocuments = _projectDocumentRepository.GetAll().Where(x => x.ProjectId == projectId);
            ViewBag.ProjectDocuments = projectDocuments;

            return RedirectToAction("_ProjectDocument", new { projectId });
        }

    }
}