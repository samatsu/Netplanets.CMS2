using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Areas.Admin.Models
{
    public class EditFavoriteLinkViewModel
    {
        [HiddenInput(DisplayValue=true)]
        public int FavoriteLinkID { get; set; }
        [Required]
        [DataType(DataType.Url)]
        [StringLength(1024)]
        public string Url { get; set; }
        [Required]
        [StringLength(256)]
        public string Text { get; set; }
        [Range(0, 1000)]
        public int SortOrder { get; set; }
    }
}