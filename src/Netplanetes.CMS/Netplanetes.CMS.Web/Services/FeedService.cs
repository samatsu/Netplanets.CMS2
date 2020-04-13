using Netplanetes.CMS.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Netplanetes.CMS.Web.Services
{

    public class FeedService : IFeedService
    {
        private IContentRepository _repository;

        public FeedService(IContentRepository repository)
        {
            this._repository = repository;
        }

        public string CreateSiteMap(UrlHelper helper)
        {
            XDocument xdoc = new XDocument();
            xdoc.Declaration = new XDeclaration("1.0", "UTF-8", "yes");
            XNamespace xn = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9");
            XElement elem = new XElement(xn + "urlset");
            var query = from a in _repository.Articles
                        orderby a.UpdatedDate descending
                        select new
                        {
                            ArticleID = a.ArticleID,
                            LID = a.LID,
                            LastMode = a.UpdatedDate
                        };
            string basepath = GetBasePath(helper);

            foreach (var item in query)
            {
                elem.Add(new XElement(xn + "url",
                    new XElement(xn + "loc", basepath + (helper.Action("Archives", "Article", new { id = item.LID }, null))),
                    new XElement(xn + "lastmod", item.LastMode.ToUniversalTime().ToString("u").Replace(" ", "T")),
                    new XElement(xn + "changefreq", "monthly"),
                    new XElement(xn + "priority", "0.5")
                    )
                );
            }
            xdoc.Add(elem);
            return xdoc.ToString();
        }

        /// <summary>
        /// RSS2.0のフィードXML文字列を生成する
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public string CreateRss20(UrlHelper helper)
        {
            SyndicationFeed feed = CreateFeed(helper, 10);
            Rss20FeedFormatter formatter = new Rss20FeedFormatter(feed);
            return CreateSyndicationXml(formatter);
        }

        /// <summary>
        /// ATOM1.0のフィードXML文字列を生成する
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public string CreateAtom10(UrlHelper helper)
        {
            SyndicationFeed feed = CreateFeed(helper, 10);
            Atom10FeedFormatter formatter = new Atom10FeedFormatter(feed);
            return CreateSyndicationXml(formatter);
        }

        /// <summary>
        /// フィードを作成する
        /// </summary>
        /// <param name="helper">URL作成用のヘルパーメソッド</param>
        /// <param name="count">フィードレコードの最大取得数</param>
        /// <returns>フィードデータ</returns>
        private SyndicationFeed CreateFeed(UrlHelper helper, int count)
        {
            // Feed データの準備
            string basepath = GetBasePath(helper);
            SyndicationFeed feed = new SyndicationFeed("Netplanetes V2", "最新の公開記事一覧", new Uri(basepath));

            List<SyndicationItem> items = new List<SyndicationItem>();
            var query = from a in _repository.Articles
                        where a.Approved == true
                        orderby a.UpdatedDate descending
                        select new
                        {
                            ArticleID = a.ArticleID,
                            LID = a.LID,
                            DisplayTitle = a.DisplayTitle,
                            Abstract = a.Abstract,
                            PublishDate = a.ReleaseDate,
                            LastUpdatedTime = a.UpdatedDate
                        };
            foreach (var article in query.Take(count))
            {
                SyndicationItem item = new SyndicationItem(article.DisplayTitle, article.Abstract,
                    new Uri(basepath + (helper.Action("Archives", "Article", new { id = article.LID }, null))),
                    article.ArticleID.ToString(), article.LastUpdatedTime);

                item.PublishDate = article.PublishDate;
                items.Add(item);
            }
            feed.Items = items;

            return feed;
        }
        /// <summary>
        /// シンジケーションフィードをXMLに変換する
        /// </summary>
        /// <param name="formatter">フォーマッタ</param>
        /// <returns>XML形式文字列</returns>
        private string CreateSyndicationXml(SyndicationFeedFormatter formatter)
        {
            // XML 出力
            StringBuilder builder = new StringBuilder();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Encoding = Encoding.UTF8;
            setting.OmitXmlDeclaration = true;
            using (XmlWriter writer = XmlWriter.Create(builder, setting))
            {
                formatter.WriteTo(writer);
                writer.Close();
            }

            return builder.ToString();
        }

        private string GetBasePath(UrlHelper helper)
        {
            return helper.RequestContext.HttpContext.Request.Url.AbsoluteUri.Replace(helper.RequestContext.HttpContext.Request.Url.PathAndQuery, "");
        }

    }
}