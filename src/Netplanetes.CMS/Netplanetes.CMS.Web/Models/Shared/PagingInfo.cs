using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Models.Shared
{
    /// <summary>
    /// ページング情報を表すクラス
    /// </summary>
    public class PagingInfo
    {
        /// <summary>全レコード数</summary>
        public int TotalRecordCount { get; set; }
        /// <summary>1ページあたりのレコード数</summary>
        public int PageSize { get; set; }
        /// <summary>カレントページ</summary>
        public int CurrentPage { get; set; }
        /// <summary>全ページ数</summary>
        public int PageCount
        {
            get { return (int)Math.Ceiling((decimal)TotalRecordCount / PageSize); }
        }

    }
}