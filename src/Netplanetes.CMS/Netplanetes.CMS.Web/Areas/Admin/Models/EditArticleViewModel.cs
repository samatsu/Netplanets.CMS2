using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Netplanetes.CMS.Web.Areas.Admin.Models
{
    public class EditArticleViewModel
    {
        public EditArticleViewModel()
        {
            this.AddedDate = DateTime.Today;
            this.ExpireDate = DateTime.MaxValue;
            this.ReleaseDate = DateTime.Today;
            this.CommentsEnabled = true;
            this.Approved = false;
            this.OnlyForMembers = false;
            this.Listed = true;
        }
        [Key]
        [HiddenInput(DisplayValue = true)]
        public int ArticleID { get; set; }
        [Required]
        [StringLength(256)]
        public string DisplayTitle { get; set; }
        [StringLength(256)]
        public string LID { get; set; }
        [StringLength(256)]
        public string Abstract
        {
            get
            {
                var fragment = this.Body ?? string.Empty;
                var text = System.Text.RegularExpressions.Regex.Replace(fragment, @"<(.|\n)*?>", "");
                text = System.Net.WebUtility.HtmlDecode(text);
                // 250で区切っていたけどおさまりがいいので200に変更
                if (text.Length > 200)
                {
                    return text.Substring(0, 200) + "...";
                }
                return text;
            }
        }
        [Required]
        [AllowHtml]
        public string Body { get; set; }
        [MaxLength(20)]
        public string Series { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ExpireDate { get; set; }
        [Required]
        [DisplayName("承認")]
        public bool Approved { get; set; }
        [Required]
        [DisplayName("リストに表示")]
        public bool Listed { get; set; }
        [Required]
        [DisplayName("コメント許可")]
        public bool CommentsEnabled { get; set; }
        [Required]
        [DisplayName("メンバーオンリー")]
        public bool OnlyForMembers { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        [HiddenInput(DisplayValue = true)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AddedDate { get; set; }

    }
}