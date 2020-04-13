using Netplanetes.CMS.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.HtmlHelpers
{
    /// <summary>
    /// ページング用のヘルパークラス
    /// </summary>
    public static class PagingHelpers
    {
        /// <summary>
        /// ページング用のリンクを作成する
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="pagingInfo">ページング情報</param>
        /// <param name="pageUrl">URL作成用のデリゲーション</param>
        /// <returns>ページングリンクの文字列</returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            
            // 先頭ページ
            if (pagingInfo.CurrentPage > 0)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
                tag.InnerHtml = "Prev";
                tag.AddCssClass("prevnext");
                result.Append(tag.ToString());
            }
            // 次ページ
            if (pagingInfo.CurrentPage < pagingInfo.PageCount)
            {
                if (pagingInfo.CurrentPage > 0)
                {
                    TagBuilder tagSpacer = new TagBuilder("span");
                    tagSpacer.SetInnerText("|");
                    result.Append(tagSpacer.ToString());
                }
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
                tag.InnerHtml = "Next";
                tag.AddCssClass("prevnext");
                result.Append(tag.ToString());
            }
            for (int i = 1; i <= pagingInfo.PageCount; i++)
            {
                // アンカータグを作成
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
        public static MvcHtmlString PageLinks2(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            TagBuilder nav = new TagBuilder("nav");
            nav.AddCssClass("text-center");
            StringBuilder result = new StringBuilder();
            result.Append(PaginationLink(pagingInfo, pageUrl));
            //result.Append(PagerLink(pagingInfo, pageUrl));
            nav.InnerHtml = result.ToString();

            return MvcHtmlString.Create(nav.ToString()) ;

        }

        public static string PaginationLink(PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");
            ul.AddCssClass("navbar-material-amber");
            StringBuilder result = new StringBuilder();

            TagBuilder liPrev = new TagBuilder("li");
            TagBuilder anchorPrev = new TagBuilder("a");
            anchorPrev.InnerHtml = "Prev";
            if (pagingInfo.CurrentPage > 0)
            {
                anchorPrev.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
            }
            else
            {
                liPrev.AddCssClass("disabled");
            }
            liPrev.InnerHtml = anchorPrev.ToString();
            result.Append(liPrev.ToString());

            for (int i = 0; i < pagingInfo.PageCount; i++)
            {
                TagBuilder li = new TagBuilder("li");
                if (i == pagingInfo.CurrentPage)
                {
                    li.AddCssClass("active");
                }
                // アンカータグを作成
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = (i+1).ToString();
                li.InnerHtml = tag.ToString();

                result.Append(li.ToString());
            }

            TagBuilder liNext = new TagBuilder("li");
            TagBuilder anchorNext = new TagBuilder("a");
            anchorNext.InnerHtml = "Next";
            if (pagingInfo.CurrentPage + 1 < pagingInfo.PageCount)
            {
                anchorNext.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
            }
            else
            {
                liNext.AddCssClass("disabled");
            }
            liNext.InnerHtml = anchorNext.ToString();
            result.Append(liNext.ToString());


            ul.InnerHtml = result.ToString();

            return ul.ToString();
        }

        /// <summary>
        /// Prev, Next リンクのみのHTML
        /// </summary>
        /// <param name="pagingInfo"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public static string PagerLink(PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pager");

            StringBuilder result = new StringBuilder();

            TagBuilder liPrev = new TagBuilder("li");
            TagBuilder anchor = new TagBuilder("a");
            anchor.InnerHtml = "Prev";
            liPrev.AddCssClass("previous");
            if (pagingInfo.CurrentPage > 1)
            {
                anchor.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
            }
            else
            {
                liPrev.AddCssClass("disabled");
            }
            liPrev.InnerHtml = anchor.ToString();

            TagBuilder liNext = new TagBuilder("li");
            anchor = new TagBuilder("a");
            anchor.InnerHtml = "Next";
            liNext.AddCssClass("next");
            if (pagingInfo.CurrentPage < pagingInfo.PageCount)
            {
                anchor.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
            }
            else
            {
                liNext.AddCssClass("disabled");
            }
            liNext.InnerHtml = anchor.ToString();

            result.Append(liPrev.ToString());
            result.Append(liNext.ToString());
            ul.InnerHtml = result.ToString();

            return ul.ToString();
        }

    }
}