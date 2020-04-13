using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Models.Content
{
    public class CommentPostViewModel
    {
        [Key, HiddenInput]
        public int ArticleID { get; set; }
        [Required]
        [StringLength(1024)]
        [DisplayName("コメント")]
        public string Body { get; set; }
        [Required]
        [StringLength(256)]
        [DisplayName("お名前")]
        public string AddedBy { get; set; }
        [StringLength(1024, MinimumLength=0)]
        [DisplayName("Webサイト")]
        public string AddedByWeb { get; set; }
        private string _addedByEmail = null;
        [DisplayName("メールアドレス")]
        [MaxLength(256)]
        [EmailAddress(ErrorMessage="メールアドレスを入力してください")]
        public string AddedByEmail
        {
            get
            {
                if (string.IsNullOrEmpty(_addedByEmail)) return null;
                return _addedByEmail;
            }
            set
            {
                _addedByEmail = value;
            }
        }

    }
}