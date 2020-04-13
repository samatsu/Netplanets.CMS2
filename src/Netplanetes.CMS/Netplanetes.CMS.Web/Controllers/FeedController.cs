using Netplanetes.CMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Netplanetes.CMS.Web.Controllers
{
    public class FeedController : Controller
    {
        IFeedService _service;
        public FeedController(IFeedService service)
        {
            _service = service;
        }
        public ActionResult SiteMap()
        {
            return Content(_service.CreateSiteMap(this.Url), "application/xml", System.Text.Encoding.UTF8);
        }
        public ContentResult Rss20()
        {
            return Content(_service.CreateRss20(this.Url), "application/rss+xml", System.Text.Encoding.UTF8);
        }
        public ContentResult Atom10()
        {
            return Content(_service.CreateAtom10(this.Url), "application/atom+xml", System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 外部サイトのRSSを取得するメソッド
        /// </summary>
        /// <param name="rssUrl"></param>
        /// <returns></returns>
        [OutputCache(Duration = 1800, VaryByParam = "rssUrl")]
        [ChildActionOnly]
        public PartialViewResult GetRssPart(string rssUrl)
        {
            if (string.IsNullOrWhiteSpace(rssUrl))
            {
                throw new ApplicationException("RSSのUrlを指定してください");
            }
            XmlReader reader = XmlReader.Create(rssUrl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            return PartialView(feed);// ビューは未実装なのでエラーが発生します。
        }

	}
}