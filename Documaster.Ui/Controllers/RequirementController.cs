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

        public RequirementController(IGenericEntityService<Requirement> entityService,
            IGenericEntityService<ProjectRequirement> projectRequirementService,
            IGenericEntityService<OutputDocument> outputDocumentService)
        {
            _requirementService = entityService;
            _projectRequirementService = projectRequirementService;
            _outputDocumentService = outputDocumentService;
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
            var model = _requirementService.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Requirement requirement)
        {
            _requirementService.Update(requirement, new List<string> { "Name" });
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
        public ActionResult SaveProject(int projectId)
        {
            var model = _requirementService.GetAll();

            var assisgnedProjectRequirements = _projectRequirementService.GetAll().Where(x => x.ProjectId == projectId);

            var reqs = _requirementService.GetAll()//.Where(x=>x.ProjectRequirements.Any())
                .Select(x => new AssignedRequirement { Assigned = x.ProjectRequirements.Any(y => y.ProjectId == projectId), Name = x.Name, Id = x.Id });
            ViewBag.Requirements = reqs;
            ViewBag.ProjectId = projectId;
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveProject(int projectId, IEnumerable<int> assignedRequirements)
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

            ViewBag.ProjectId = projectId;
            return View(model2);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase fileUpload, int projectId, int requirementId)
        {
            var length = fileUpload.ContentLength;
            byte[] tempImage = new byte[length];
            fileUpload.InputStream.Read(tempImage, 0, length);
           // newImage.ActualImage = tempImage;


            var output = new OutputDocument {
                Name = fileUpload.FileName,
                DocumentData = tempImage,
                ProjectId = projectId,
                RequirementId = requirementId

            };
            var created = _outputDocumentService.Create(output);

            // return new HttpStatusCodeResult(HttpStatusCode.OK);
            return RedirectToAction("CustomerProject",new { projectId = projectId});
        }


    }
}