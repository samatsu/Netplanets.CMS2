using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Areas.Admin.Models
{
    public class EditUserViewModel
    {
        [Required]
        [HiddenInput()]
        public string ID { get; set; }
        [Required]
        [HiddenInput(DisplayValue=true)]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}