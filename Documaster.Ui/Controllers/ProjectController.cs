using Documaster.Business.Services;
using Documaster.Business.Extensions;
using Documaster.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net;

namespace Documaster.Ui.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<ProjectRequirement> _projectRequirementRepository;

        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = unitOfWork.Repository<Project>();
            _customerRepository = unitOfWork.Repository<Customer>();
            _projectRequirementRepository = unitOfWork.Repository<ProjectRequirement>();
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
                    ? _projectRepository.GetAll().OrderByDescending(sortProperty).ToList()
                    : _projectRepository.GetAll().OrderBy(sortProperty).ToList();

            ViewBag.SortDescending = sortDescending;
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
            _projectRepository.Create(project);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _projectRepository.Get(id);
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
                _customerRepository.Create(customer);
            }
            else
            {
                _customerRepository.Update(project.Customer, new List<string> { "Name", "Telephone", "Email", "Address" });
                _projectRepository.Update(project, new List<string> { "Number" });
            }

            _projectRepository.Update(project, new List<string> { "Name", "Expire" });
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _projectRepository.Get(id);
            var pR = _projectRequirementRepository.Get(x => x.ProjectId == id);
            ViewBag.ProjectId = pR;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Project project)
        {
            var dbProjectRequirements = _projectRequirementRepository.Get( x => x.ProjectId == project.Id );
            foreach (var item in dbProjectRequirements)
            {
                _projectRequirementRepository.Delete(item.Id);
            }

            var customers = _customerRepository.Get(x => x.Id == project.Id);
            foreach (var item in customers)
            {
                _customerRepository.Delete(item.Id);
            }

            _projectRepository.Delete(project.Id);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToggleProjectState(int projectId, bool state)
        {
            var project = _projectRepository.Get(projectId);
            if (project == null)
            {
                return HttpNotFound();
            }

            project.IsReady = state;
            _projectRepository.Update(project, new List<string> {"IsReady"});
            _unitOfWork.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}