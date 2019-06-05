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
                var imagesDirectory = Server.MapPath("~/Images/");
                DeleteAllFilesInFolder(imagesDirectory);

                var filename = Path.GetFileName(fileUpload.FileName);
                var filepath = Path.Combine(imagesDirectory, filename);
                fileUpload.SaveAs(filepath);
            }
            return RedirectToAction("Index", "Project");
        }

        private static void DeleteAllFilesInFolder(string imagesDirectory)
        {
            var directory = new DirectoryInfo(imagesDirectory);
            foreach (var fileInfo in directory.GetFiles())
            {
                fileInfo.Delete();
            }
        }
    }
}
