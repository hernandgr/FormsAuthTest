using FormsAuthTest.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace FormsAuthTest.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = FormsAuthentication.Authenticate(model.User, model.Password);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid user and password");
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(model.User, false);
            return RedirectToLocal(returnUrl);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            // TODO - Remove forms authentication
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}