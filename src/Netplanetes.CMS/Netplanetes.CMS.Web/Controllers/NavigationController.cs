using Netplanetes.CMS.Web.Models.Shared;
using Netplanetes.CMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Controllers
{
    public class NavigationController : Controller
    {
        private INavigationService _service;
        public NavigationController(INavigationService service)
        {
            _service = service;
        }

        public PartialViewResult CategoriesLinks()
        {
            return PartialView(_service.RetrieveCatetoriesLinks());
        }

        public PartialViewResult CategoriesLinksWithArticleCount()
        {
            return PartialView(_service.RetrieveCategoriesLinksWithArticleCount());
        }
        public PartialViewResult LatestArticlesLinks(int? count)
        {
            IEnumerable<ActionLinkViewModel> list = null;
            if (count.HasValue)
            {
                list = _service.RetrieveLatestArticlesLinks(count.Value);
            }
            else
            {
                list = _service.RetrieveLatestArticlesLinks();
            }
            return PartialView(list);
        }
        public PartialViewResult FavoriteLinks()
        {
            return PartialView(_service.RetrieveFavoriteLinks());
        }
        public PartialViewResult SideAdsense()
        {
            return PartialView();
        }

        /// <summary>
        /// グローバルメニュー用のカテゴリーの一覧を返す
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Header()
        {
            return PartialView(_service.RetrieveCatetoriesLinks());
        }
    }
}