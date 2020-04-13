using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Areas.Admin.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [HiddenInput()]
        public string ID { get; set; }
        [Required]
        [HiddenInput(DisplayValue = true)]
        public string UserName { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword")]
        public string NewPasswordConfirm { get; set; }
    }
}