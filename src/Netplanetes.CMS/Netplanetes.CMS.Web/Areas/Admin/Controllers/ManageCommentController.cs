using Netplanetes.CMS.Web.Areas.Admin.Models;
using Netplanetes.CMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageCommentController : Controller
    {
        IContentService _service = null;
        public ManageCommentController(IContentService service)
        {
            _service = service;
        }
        public ActionResult List(int page = 0)
        {
            if (page < 0) page = 0;

            return View(_service.RetrieveComments(page, 10));
        }

        public ActionResult Delete(int commentid, string returnUrl)
        {
            if (commentid == 0) return RedirectToAction("List");

            _service.DeleteComment(commentid);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var comment = _service.GetCommentById(id);
            if (comment == null)
            {
                return RedirectToAction("List");
            }
            return View(comment);
        }
        [HttpPost]
        public ActionResult Edit(EditCommentViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateComment(model, User.Identity);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("List");
            }
            return View(model);
        }
    }
}