using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Documaster.Ui.Controllers
{
    [Authorize]
    public class HelpController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }
    }
}
