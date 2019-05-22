using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Documaster.Business.Models;
using Documaster.Business.Services;

namespace Documaster.Ui.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISecurityService _securityService;

        public AccountController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (!_securityService.UserExists())
            {
                var hasCreatedUser = _securityService.CreateUserAndPassword(loginModel);
                if (!hasCreatedUser)
                {
                    ModelState.AddModelError("", "Nu am putut crea utilizator si/sau parola!");
                }
            }
            var hasLoggedIn = _securityService.ValidateUserAndPassword(loginModel);
            if (hasLoggedIn)
            {
                FormsAuthentication.SetAuthCookie(loginModel.UserName, loginModel.RememberMe);
                return RedirectToAction("Index","Project");
            }
            ModelState.AddModelError("", "Nume sau parola gresite.");
            return View(loginModel);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Welcome", "Project");
        }
    }
}