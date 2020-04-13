using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Netplanetes.CMS.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Netplanetes.IdentityModel;
using Netplanetes.CMS.Web.Services;

namespace Netplanetes.CMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAuthService _service;
        public AccountController(IAuthService service)
        {
            _service = service;
        }
        public ActionResult Logout(string returnUrl = "~/")
        {
            AuthManager.SignOut();
            return Redirect(returnUrl);
        }
        public ActionResult Login(string returnUrl = "~/")
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl = "~/")
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = await _service.CreateIdentity(model);
                if (identity == null)
                {
                    ModelState.AddModelError("", "ユーザー名やパスワードが一致しません");
                }
                else
                {
                    AuthManager.SignOut();

                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    }, identity);
                    return Redirect(returnUrl);
                }

            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}