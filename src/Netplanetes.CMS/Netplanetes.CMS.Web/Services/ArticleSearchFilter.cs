using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Services
{
    public class ArticleSearchFilter
    {
        #region コンストラクタ
        /// <summary>
        /// フィールドをデフォルト値で初期化する
        /// </summary>
        public ArticleSearchFilter()
        {
            this.CategoryLID = null;
            this.PageNum = 0;
            this.PageSize = 10;
            this.IsManageMode = false;
            this.IncludeShowMemberOnly = false;
            this.IncludeNonListed = false;
        }
        #endregion

        /// <summary>
        /// 外部サイトで記事一覧を検索するときのデフォルト設定でインスタンスを生成するファクトリメソッド
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="isAuthenticated"></param>
        public static ArticleSearchFilter CreateForPublic(string categoryLinkTitle, bool isAuthenticated)
        {
            ArticleSearchFilter filter = new ArticleSearchFilter
            {
                CategoryLID = categoryLinkTitle,
                IsManageMode = false,
                IncludeShowMemberOnly = isAuthenticated,
                IncludeNonListed = false
            };

            return filter;
        }
        /// <summary>
        /// 管理モード記事一覧を検索するときのでふぉうと設定でインスタンスを生成するファクトリメソッド
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static ArticleSearchFilter CreateForManage(string categoryLinkTitle)
        {
            ArticleSearchFilter filter = new ArticleSearchFilter
            {
                CategoryLID = categoryLinkTitle,
                IsManageMode = true,
                IncludeShowMemberOnly = true,
                IncludeNonListed = true
            };
            return filter;
        }
        #region　フィルタ用のプロパティ
        /// <summary>
        /// カテゴリのリンクタイトル
        /// </summary>
        public string CategoryLID { get; set; }
        /// <summary>
        /// 記事のシリーズタグ
        /// </summary>
        public string Series { get; set; }
        /// <summary>
        /// ページ番号
        /// </summary>
        public int PageNum { get; set; }
        /// <summary>
        /// 1ページあたりのレコード数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Adminモードがtrueの場合
        /// カテゴリ、ページ番号、ページサイズ、ソート
        /// 以外の情報は無視される
        /// </summary>
        public bool IsManageMode { get; set; }
        /// <summary>
        /// メンバーのみレコードを含めるか <br/>
        /// IsManageModeがtrueの場合はこの設定は無視される
        /// 
        /// </summary>
        public bool IncludeShowMemberOnly { get; set; }
        /// <summary>
        /// リスト一覧表示が無効な記事を含めるか<br/>
        /// IsManageModeがtrueの場合はこの設定は無視される
        /// </summary>
        public bool IncludeNonListed { get; set; }
        #endregion
    }
}