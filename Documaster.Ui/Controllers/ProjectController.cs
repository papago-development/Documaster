using Documaster.Business.Services;
using Documaster.Model.Entities;
using Documaster.Ui.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Documaster.Ui.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IGenericEntityService<Project> _entityService;
        private readonly IGenericEntityService<Customer> _customerService;
        private readonly IGenericEntityService<ProjectRequirement> _projectRequirementService;

        public ProjectController(IGenericEntityService<Project> entityService,
            IGenericEntityService<Customer> customerService, IGenericEntityService<ProjectRequirement>
            projectRequirementService)
        {
            _entityService = entityService;
            _customerService = customerService;
            _projectRequirementService = projectRequirementService;

        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _entityService.GetAll().ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            _entityService.Create(project);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _entityService.Get(id);
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
                    Email = project.Customer.Email,
                    Address = project.Customer.Address
                };
                _customerService.Create(customer);
            }
            else
            {

                _customerService.Update(project.Customer, new List<string> { "Name", "Telephone", "Email", "Address" });
            }

            _entityService.Update(project, new List<string> { "Name" });



            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _entityService.Get(id);
            var pR = _projectRequirementService.Get(x => x.ProjectId == id).ToList();
            ViewBag.ProjectId = pR;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Project project, IEnumerable<int> assignedRequirements)
        {
            //   _projectRequirementService.Delete(project.Id);

            var dbProjectRequirements = _projectRequirementService.Get(x => x.ProjectId == project.Id).ToList();
            var deletedProjectRequirements = dbProjectRequirements
                .Where(x => assignedRequirements == null || !assignedRequirements.Any(y => y == x.RequirementId)).ToList();

            var projectIdRequirement = _projectRequirementService.GetAll().Where(x => x.ProjectId == project.Id).ToList();

            var customers = _customerService.GetAll().Where(x => x.Id == project.Id).ToList();

            foreach (var item in deletedProjectRequirements)
            {
                _projectRequirementService.Delete(item.Id);
            }

            foreach (var item in projectIdRequirement)
            {
                _projectRequirementService.Delete(item.ProjectId);
                _projectRequirementService.Delete(item.RequirementId);

            }

            foreach (var item in customers)
            {
                _customerService.Delete(item.Id);
            }

            _entityService.Delete(project.Id);
            ViewBag.ProjectId = projectIdRequirement;

            return RedirectToAction("Index");
        }
    }
}