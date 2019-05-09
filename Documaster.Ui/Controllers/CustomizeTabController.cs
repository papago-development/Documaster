using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NLog;
using System;

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
            var model = _customizeTabService.GetCustomizeTabById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CustomizeTab customizeTab)
        {
            if (ModelState.IsValid)
            {
                _customizeTabService.Edit(customizeTab);
                return RedirectToAction("Index");
            }

            return View(customizeTab);
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
            //try
            //{
                _customizeTabService.SaveOrder(sortedList, entityName);

                //_logger.Debug("Test");
                //_logger.Info("Test");
                //_logger.Warn("Test");

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            //}catch (Exception ex)
            //{
            //    _logger.Error(ex, "Test Error");
            //    _logger.Fatal(ex, "Test Fatal");
            //    throw;
            //}
        }
    }
}
