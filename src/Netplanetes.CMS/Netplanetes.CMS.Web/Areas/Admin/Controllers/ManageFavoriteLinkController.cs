using Netplanetes.CMS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageFavoriteLinkController : Controller
    {
        private Netplanetes.CMS.Web.Services.IContentService  _service = null;
        public ManageFavoriteLinkController(Netplanetes.CMS.Web.Services.IContentService service)
        {
            _service = service;
        }
        /// <summary>
        /// お気に入りリンク一覧ページ
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View(_service.RetrieveFavoriteLinks());
        }
        [HttpPost]
        public ActionResult Delete(int favoriteLinkId, string returnUrl)
        {
            _service.DeleteFavoriteLink(favoriteLinkId);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View("Edit", new EditFavoriteLinkViewModel());
        }

        [HttpPost]
        public ActionResult Create(EditFavoriteLinkViewModel model)
        {
            if (ModelState.IsValid)
            {
                _service.CreateFavoriteLink(model,User.Identity);
                return RedirectToAction("List");
            }
            return View("Edit", model);
        }
        public ActionResult Edit(int id)
        {
            return View(_service.GetFavoriteLinkById(id));

        }
        [HttpPost]
        public ActionResult Edit(EditFavoriteLinkViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateFavoriteLink(model, User.Identity);
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