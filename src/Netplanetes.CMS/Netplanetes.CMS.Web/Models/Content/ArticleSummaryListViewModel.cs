using Netplanetes.CMS.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Models.Content
{
    public class ArticleSummaryListViewModel
    {
        /// <summary>
        /// 記事一覧で表示しているカテゴリーリンクタイトル
        /// </summary>
        public string CategoryLID { get; set; }
        /// <summary>
        /// 記事一覧で表示しているカテゴリー名
        /// </summary>
        public string CategoryDisplayTitle { get; set; }
        /// <summary>
        /// シリーズタグ
        /// </summary>
        public string Series { get; set; }
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