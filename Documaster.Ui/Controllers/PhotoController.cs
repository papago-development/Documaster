using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.Enums;
using System.IO;

namespace Documaster.Ui.Controllers
{
    public class PhotoController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase fileUpload)
        {
            if (fileUpload.ContentLength > 0)
            {
                string filename = Path.GetFileName(fileUpload.FileName);
                string filepath = Path.Combine(Server.MapPath("~/Images/"), filename);
                fileUpload.SaveAs(filepath);
            }
            return RedirectToAction("Welcome", "Project");
        }
    }
}
