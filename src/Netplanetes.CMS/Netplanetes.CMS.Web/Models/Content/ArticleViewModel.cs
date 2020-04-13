using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Models.Content
{
    /// <summary>
    /// 外部に公開する記事のページを表すビューモデル
    /// </summary>
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
            this.CommentForm = new CommentPostViewModel();
        }
        [HiddenInput(DisplayValue = false)]
        public int ArticleID { get; set; }
        public string PreviousArticleLID { get; set; }
        public string NextArticleLID { get; set; }
        public string DisplayTitle { get; set; }
        public string LID { get; set; }
        public string Series { get; set; }
        public string Body { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool CommentsEnabled { get; set; }
        public int ViewCount { get; set; }
        public int Votes { get; set; }
        public int TotalRating { get; set; }
        public string AddedBy { get; set; }
        public string AverageRating
        {
            get
            {
                if (Votes <= 0) return "N/A";
                return Math.Round(TotalRating / (decimal)Votes, 2).ToString();
            }
        }
        public CommentPostViewModel CommentForm { get; set; }
    }
}