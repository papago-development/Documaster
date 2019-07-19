using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System;
using Documaster.Model.Enums;
using System.Web.Mvc.Html;

namespace Documaster.Ui.Controllers
{
    public class CustomizeTabController : Controller
    {
        private readonly ICustomizeTabService _customizeTabService;
        private readonly INamedEntityService<CustomizeTab> _namedEntityService;

        public CustomizeTabController(ICustomizeTabService customizeTabService,
                                      INamedEntityService<CustomizeTab> namedEntityService)
        {
            _customizeTabService = customizeTabService;
            _namedEntityService = namedEntityService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _customizeTabService.GetCustomizeTabs().OrderBy(x=>x.Number);
            ViewBag.CustomizeTabs = model;
            return View (model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var documentTypeList = new List<SelectListItem>();
            documentTypeList.Add(new SelectListItem
            {
                Text = "Selecteaza tipul documentului",
                Value = ""
            });
            foreach (DocumentType eVal in Enum.GetValues(typeof(DocumentType)))
            {
                documentTypeList.Add(new SelectListItem { Text = Enum.GetName(typeof(DocumentType), eVal), Value = eVal.ToString() });
            }
            ViewBag.DocumentsType = documentTypeList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomizeTab customizeTab)
        {
            _customizeTabService.Create(customizeTab);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id) 
        {
             var documentTypeList = new List<SelectListItem>();
            documentTypeList.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (DocumentType eVal in Enum.GetValues(typeof(DocumentType)))
            {
                documentTypeList.Add(new SelectListItem { Text = Enum.GetName(typeof(DocumentType), eVal), Value = eVal.ToString() });
            }
            ViewBag.DocumentsType = documentTypeList;

            var model = _customizeTabService.GetCustomizeTabById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CustomizeTab customizeTab)
        {
                _customizeTabService.Edit(customizeTab);
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _customizeTabService.GetCustomizeTabById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(CustomizeTab customizeTab)
        {
            _customizeTabService.Delete(customizeTab);
            return RedirectToAction("Index");
        }

        public JsonResult DoesNumberExist(CustomizeTab customizeTab)
        {
            var doesNumberExist = _customizeTabService.DoesNumberExist(customizeTab);
            return Json(!doesNumberExist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DoesNameExist(CustomizeTab customizeTab)
        {
            var doesNameExist = _namedEntityService.DoesNameExist(customizeTab);
            return Json(!doesNameExist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveOrder(IList<int> sortedList, string entityName)
        {
            _customizeTabService.SaveOrder(sortedList, entityName);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
