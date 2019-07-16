using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;

namespace Documaster.Ui.Controllers
{
    [Authorize]
    public class TemplateController : Controller
    {
        private readonly ITemplateService _templateService;

        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = _templateService.GetTemplates();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Template template)
        {
            if (ModelState.IsValid)
            {
                _templateService.CreateTemplate(template);
                var isCreated = true;
                TempData["CreateSuccess"] = isCreated;
                return RedirectToAction("Index");
            }
            return View(template);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _templateService.GetTemplateById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Template template)
        {
            _templateService.UpdateTemplate(template);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _templateService.GetTemplateById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Template template)
        {
            var isDeleted = _templateService.DeleteTemplate(template);
            TempData["DeleteSuccess"] = isDeleted;
            return RedirectToAction("Index");
        }

        public JsonResult DoesNameExist(Template template)
        {
            var doesNameExist = _templateService.DoesNameExist(template);
            return Json(!doesNameExist, JsonRequestBehavior.AllowGet);
        }
    }
}