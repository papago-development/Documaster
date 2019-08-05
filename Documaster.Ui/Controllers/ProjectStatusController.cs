using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;

namespace Documaster.Ui.Controllers
{
    public class ProjectStatusController : Controller
    {
        private readonly IProjectStatusService _projectStatusService;
        private readonly INamedEntityService<ProjectStatus> _namedEntityService;

        public ProjectStatusController(IProjectStatusService projectStatusService, INamedEntityService<ProjectStatus> namedEntityService)
        {
            _projectStatusService = projectStatusService;
            _namedEntityService = namedEntityService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var statuses = _projectStatusService.GetAll();
            return View(statuses);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProjectStatus projectStatus)
        {
            if (ModelState.IsValid)
            {
                _projectStatusService.CreateProjectStatus(projectStatus);
                return RedirectToAction("Index");
            }
            return View(projectStatus);
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
            var isDeleted = _projectStatusService.DeleteProjectStatus(projectStatus);
            TempData["DeleteSuccess"] = isDeleted;
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

        public JsonResult DoesNameExist(ProjectStatus projectStatus)
        {
            var doesNameExist = _namedEntityService.DoesNameExist(projectStatus);
            return Json(!doesNameExist, JsonRequestBehavior.AllowGet);
        }
    }
}
