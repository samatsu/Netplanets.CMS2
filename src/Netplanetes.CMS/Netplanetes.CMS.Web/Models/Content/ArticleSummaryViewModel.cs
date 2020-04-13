using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Models.Content
{
    public class ArticleSummaryViewModel
    {
        /// <summary>
        /// 表示タイトル
        /// </summary>
        public string DisplayTitle { get; set; }
        /// <summary>
        /// リンク用のタイトル
        /// </summary>
        public string LID { get; set; }
        /// <summary>
        /// 概要
        /// </summary>
        public string Abstract { get; set; }
        /// <summary>
        /// 公開日
        /// </summary>
        [DisplayFormat(DataFormatString="{0;d}")]
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// 記事のアイコンクラス
        /// </summary>
        public string CssClass { get; set; }
        /// <summary>
        /// カテゴリの表示名
        /// </summary>
        public string CategoryTitle { get; set; }
        /// <summary>
        /// ページビュー数
        /// </summary>
        public int ViewCount { get; set; }
        /// <summary>
        /// 承認状態
        /// </summary>
        public bool Approved { get; set; }
        /// <summary>
        /// 評価数の合計
        /// </summary>
        public int Votes { get; set; }
        /// <summary>
        /// 評価ポイントの合計
        /// </summary>
        public int TotalRating { get; set; }
        /// <summary>
        /// コメント数
        /// </summary>
        [Range(0, int.MaxValue)]
        public int CommentCount { get; set; }
        /// <summary>
        /// 平均評価をあらくぁす文字列
        /// </summary>
        public string AvarageRatingString
        {
            get
            {
                if (Votes == 0)
                {
                    return "-";
                }
                else
                {
                    return Math.Round(TotalRating / (double)Votes, 2).ToString();
                }
            }
        }
    }
}