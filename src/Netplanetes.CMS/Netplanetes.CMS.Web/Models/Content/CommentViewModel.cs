using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Models.Content
{
    /// <summary>
    /// 外部サイトに表示するコメントを表すビューモデル
    /// </summary>
    public class CommentViewModel
    {
        [Key,HiddenInput(DisplayValue=true)]
        public int CommentID { get; set; }
        public int ArticleID { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime AddedDate { get; set; }
        [Required]
        public string AddedBy { get; set; }
        [DataType(DataType.Url)]
        public string AddedByWeb { get; set; }
    }
}