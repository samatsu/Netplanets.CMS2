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
    /// 編集用カテゴリビューモデル
    /// </summary>
    public class EditCategoryViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue=true)]
        public int CategoryID { get; set; }
        [Required]
        [StringLength(256)]
        public string DisplayTitle { get; set; }
        [StringLength(256)]
        public string LID { get; set; }
        [Required]
        [StringLength(256)]
        public string CssClass { get; set; }
        [Required]
        [Range(0, 10000)]
        public int SortOrder { get; set; }
        [Required]
        [StringLength(32)]
        public string ShortDescription { get; set; }
        [Required]
        [StringLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}