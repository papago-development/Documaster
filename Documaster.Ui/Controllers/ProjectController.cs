using Documaster.Business.Services;
using Documaster.Business.Extensions;
using Documaster.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Documaster.Ui.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<ProjectRequirement> _projectRequirementRepository;

        private readonly IProjectService _projectService;
        private readonly IProjectRequirementService _projectRequirementService;
        private readonly ICustomerService _customerService;
        private readonly IProjectStatusService _projectStatusService;

        public ProjectController(IUnitOfWork unitOfWork, 
                                 IProjectService projectService, 
                                 IProjectRequirementService projectRequirementService,
                                 ICustomerService customerService,
                                 IProjectStatusService projectStatusService)
        {
            _unitOfWork = unitOfWork;
            _projectRequirementRepository = unitOfWork.Repository<ProjectRequirement>();

            _projectService = projectService;
            _projectRequirementService = projectRequirementService;
            _customerService = customerService;
            _projectStatusService = projectStatusService;
        }

        [HttpGet]
        public ActionResult Welcome()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List", new {sortProperty = "Expire", sortDescending = true});
        }

        [HttpGet]
        public ActionResult List(string sortProperty, bool sortDescending)
        {
            var model = sortDescending
                    ? _projectService.GetAllProjects().OrderByDescending(sortProperty).ToList()
                    : _projectService.GetAllProjects().OrderBy(sortProperty).ToList();

            var projectStatusList = _projectStatusService.GetAll();
            ViewBag.ProjectStatuses = projectStatusList;
            ViewBag.SortDescending = sortDescending;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Populate dropdownlist with project status 
            var projectStatusList = _projectStatusService.GetAll();
            ViewBag.ProjectStatuses = projectStatusList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            _projectService.CreateProject(project);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var projectStatusesList = _projectStatusService.GetAll();
            ViewBag.ProjectStatuses = projectStatusesList;

            var model = _projectService.GetProjectById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (project.Customer.Id == 0)
            {
                var customer = new Customer
                {
                    Id = project.Id,
                    Name = project.Customer.Name,
                    Telephone = project.Customer.Telephone,
                    AdditionalInfo1 = project.Customer.AdditionalInfo1,
                    AdditionalInfo2 = project.Customer.AdditionalInfo2,
                    Email = project.Customer.Email,
                    Address = project.Customer.Address
                };
                _customerService.CreateCustomer(customer);
            }
            else
            {
                _customerService.UpdateCustomer(project.Customer);
                _projectService.UpdateProject(project);
            }

           _projectService.UpdateProject(project);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _projectService.GetProjectById(id);
            var pR = _projectRequirementService.GetRequirementsById(id);
             ViewBag.ProjectId = pR;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Project project)
        {
            var dbProjectRequirements = _projectRequirementService.GetProjectRequirementByProjectId(project.Id);
            foreach (var item in dbProjectRequirements)
            {
                _projectRequirementService.DeleteProjectRequirement(item);
            }

            var customers = _customerService.GetCustomersByProject(project);
            foreach (var item in customers)
            {
                _customerService.Delete(item);
            }

            _projectService.DeleteProject(project);
   
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToggleProjectState(int projectId, bool state)
        {
            var project = _projectService.GetProjectById(projectId);
            if (project == null)
            {
                return HttpNotFound();
            }

            //project.IsReady = state;
            //_projectRepository.Update(project, new List<string> {"IsReady"});
            //_unitOfWork.SaveChanges();
            _projectService.UpdateProject(project);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult ChangeProjectStatus(int projectId, int projectStateId)
        {
            var project = _projectService.GetProjectById(projectId);
            if(project == null)
            {
                return HttpNotFound();
            }

            project.ProjectStatusId = projectStateId;
            _projectService.UpdateProject(project);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}