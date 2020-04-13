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
    public class ManageCategoryController : Controller
    {
        IContentService _service;
        public ManageCategoryController(IContentService service)
        {
            _service = service;
        }
        /// <summary>
        /// カテゴリー一覧を取得
        /// カテゴリーは多くないのでページングしない
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult List()
        {
            return View(_service.RetrieveCategories());
        }
        public ActionResult Create()
        {
            return View("Edit", new EditCategoryViewModel());
        }
        [HttpPost]
        public ActionResult Create(EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _service.CreateCategory(model, User.Identity);
                return RedirectToAction("List");
            }
            return View("Edit", model);
        }
        public ActionResult Edit(string id)
        {
            return View(_service.GetCategoryByLinkTitle(id));

        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateCategory(model, User.Identity);
                if(!string.IsNullOrEmpty(returnUrl)){
                    return Redirect(returnUrl);
                }
                return RedirectToAction("List");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(string categoryId, string returnUrl)
        {
            _service.DeleteCategory(categoryId);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("List");
        }
    }
}