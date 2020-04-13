using Netplanetes.CMS.Web.Models.Content;
using Netplanetes.CMS.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Models.Search
{
    public class SearchResultViewModel
    {
        public string Query { get; set; }
        /// <summary>
        /// 記事のサマリ一覧
        /// </summary>
        public IEnumerable<ArticleSummaryViewModel> Articles { get; set; }
        /// <summary>
        /// ページング情報
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

    }
}