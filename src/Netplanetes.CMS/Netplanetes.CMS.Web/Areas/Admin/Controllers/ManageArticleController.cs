using Netplanetes.CMS.Web.Areas.Admin.Models;
using Netplanetes.CMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Areas.Admin.Controllers
{
    [Authorize()]
    public class ManageArticleController : Controller
    {
        IContentService _service;
        ICategoryMappingService _categoryService;
        public ManageArticleController(IContentService service, ICategoryMappingService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }
        /// <summary>
        /// 記事一覧を取得
        /// </summary>
        /// <param name="page">表示ページ</param>
        /// <returns></returns>
        public ActionResult List(string id, int page = 0)
        {
            if (page < 0) page = 0;
            var filter = ArticleSearchFilter.CreateForManage(id);
            filter.PageNum = page;
            var list = _service.RetrieveArticleList(filter);
            return View(list);
        }
        /// <summary>
        /// 記事を作成する
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.CategoryList = _categoryService.CategoriesLinks;
            return View("Edit", new EditArticleViewModel());
        }
        /// <summary>
        /// 記事を作成する
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(EditArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                _service.CreateArticle(model, User.Identity);
                return RedirectToAction("List");
            }
            ViewBag.CategoryList = _categoryService.CategoriesLinks;
            return View("Edit", model);
        }
        /// <summary>
        /// 記事を編集する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            ViewBag.CategoryList = _categoryService.CategoriesLinks;
            return View(_service.GetArticleByLinkTitle(id));
        }
        [HttpPost]
        public ActionResult Edit(EditArticleViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateArticle(model, User.Identity);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("List");
            }
            ViewBag.CategoryList = _categoryService.CategoriesLinks;
            return View(model);
        }
        /// <summary>
        /// 記事を削除する
        /// </summary>
        /// <param name="artickeId">アーティクルID</param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string articleId, string returnUrl)
        {
            _service.DeleteArticle(articleId);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Approve(string id, string returnUrl)
        {
            EditArticleViewModel model =  _service.GetArticleByLinkTitle(id);
            if (model != null)
            {
                model.Approved = true;
                _service.UpdateArticle(model, User.Identity);
            }
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("List");
        }
        public JsonResult Seriestags()
        {
            var tags = _service.RetrieveSeriesTags();

            return Json(tags.ToArray());
        }
    }
}