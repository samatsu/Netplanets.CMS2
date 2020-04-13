using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Netplanetes.CMS.Web.Areas.Admin.Models;
using Netplanetes.CMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Areas.Admin.Controllers
{
    [Authorize()]
    public class ManageAccountController : Controller
    {
        private IAccountService _service;
        public ManageAccountController(IAccountService service)
        {
            _service = service;
        }
        //
        // GET: /Admin/ManageAccount/
        public ActionResult List()
        {
            return View( _service.RetrieveUsers());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _service.CreateUser(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id, string returnUrl)
        {
            IdentityResult result = await _service.DeleteUser(id);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                Redirect(returnUrl);
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(string id)
        {
            EditUserViewModel model = await _service.FindUserById(id);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _service.UpdateUser(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        public async Task<ActionResult> ChangePassword(string id)
        {
            EditUserViewModel user = await _service.FindUserById(id);
            if (user != null)
            {
                ChangePasswordViewModel model = new ChangePasswordViewModel()
                {
                    ID = user.ID,
                    UserName = user.UserName
                };
                return View(model);
            }
            return RedirectToAction("List");

        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                IdentityResult result = await _service.ChangePassword(model.ID, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}