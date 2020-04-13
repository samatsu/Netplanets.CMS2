using Netplanetes.CMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Controllers
{
    public class ArticleController : Controller
    {
        protected ISearchService _search;
        protected IContentService _service;
        public ArticleController(IContentService service, ISearchService search)
        {
            _service = service;
            _search = search;
        }
        /// <summary>
        /// トップページ
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// カテゴリに関連する記事の一覧を表示する
        /// カテゴリが未指定の場合は、最新記事を取得する
        /// </summary>
        /// <param name="id">カテゴリの名前かID</param>
        /// <returns></returns>
        public ActionResult Archives(string id, int page = 0)
        {
            // チェック
            if (page < 0) page = 0;

            var filter = ArticleSearchFilter.CreateForPublic(id, Request.IsAuthenticated);
            filter.PageNum = page;
            var model = _service.RetrieveArticleList(filter);
            return View(model);
        }
        /// <summary>
        /// 記事を表示する
        /// </summary>
        /// <param name="id">記事のタイトルもしくはID</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<ActionResult> Archive(string id)
        {
            var model = _service.RetrieveArticleByLinkTitle(id, Request.IsAuthenticated);
            if (model != null)
            {
                bool result = await _service.IncrementViewCountAsync(model.ArticleID);
                model.ViewCount++; // DBに書き込んだビューカウントはメモリ上のオブジェクトには反映されないので、1加えておく。
            }
            return View(model);
        }
        /// <summary>
        /// シリーズタグでグルーピングされた記事一覧を検索する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Series(string id, int page = 0)
        {
            // チェック
            if (page < 0) page = 0;

            var filter = ArticleSearchFilter.CreateForPublic(null, Request.IsAuthenticated);
            filter.Series = id;
            filter.PageNum = page;
            var model = _service.RetrieveArticleList(filter);
            return View("Archives", model);
        }
        /// <summary>
        /// 検索を行うプロバイダー
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public ActionResult Search(string q, int page = 0)
        {
            var model = _search.Search(q, page);
            return View(model);
        }
    }
}