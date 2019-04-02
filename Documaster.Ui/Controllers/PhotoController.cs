using System.Web;
using System.Web.Mvc;
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
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                var filename = Path.GetFileName(fileUpload.FileName);
                var filepath = Path.Combine(Server.MapPath("~/Images/"), filename);
                fileUpload.SaveAs(filepath);
            }
            return RedirectToAction("Welcome", "Project");
        }
    }
}
