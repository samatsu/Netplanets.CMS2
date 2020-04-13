using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Areas.Admin.Models
{
    /// <summary>
    /// コメントの管理用ビューモデル
    /// </summary>
    public class EditCommentViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue=true)]
        public int CommentID { get; set; }
        [Required]
        [StringLength(1024)]
        public string Body { get; set; }
        [Required]
        [StringLength(256)]
        public string AddedBy { get; set; }
        private string _addedByEmail = null;
        [StringLength(256)]
        [EmailAddress]
        public string AddedByEmail
        {
            get
            {
                if (string.IsNullOrEmpty(_addedByEmail))
                {
                    return null;
                }
                return _addedByEmail;
            }
            set
            {
                _addedByEmail = value;
            }
        }
        [StringLength(40)]
        public string AddedByIP { get; set; }
        [DataType(DataType.Url)]
        [StringLength(1024)]
        public string AddedByWeb { get; set; }
        [Required]
        [HiddenInput(DisplayValue = true)]
        public DateTime AddedDate { get; set; }

        public string ArticleLID { get; set; }
    }
}