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

        public ProjectController(IGenericEntityService<Project> entityService, IGenericEntityService<Customer> customerService)
        {
            _entityService = entityService;
            _customerService = customerService;
            //_entityProjectTypeService = entityProjectTypeService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _entityService.GetAll().ToList();
            //  var customer = _customerService.Get(1);
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
            //var project = new Project
            //{
            //    ProjectTypeId = projectViewModel.Project.ProjectTypeId,
            //    CustomerId = projectViewModel.Project.CustomerId,
            //    Name = projectViewModel.Project.Name

            //};


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
            //var customer = new Customer
            //{
            //    Id = project.Customer.Id,
            //    Name = project.Customer.Name,
            //    Telephone = project.Customer.Telephone,
            //    Email = project.Customer.Email,
            //    Address = project.Customer.Address
            //};


            _entityService.Update(project, new List<string> { "Name" });
            _customerService.Update(project.Customer, new List<string> { "Name", "Telephone", "Email", "Address" });




            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _entityService.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Project project)
        {
            _entityService.Delete(project.Id);
            return RedirectToAction("Index");
        }
    }
}