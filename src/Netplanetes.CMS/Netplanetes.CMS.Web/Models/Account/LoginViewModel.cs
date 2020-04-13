using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("アカウント")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("パスワード")]
        public string Password { get; set; }
        [Required]
        [DisplayName("ログインしたままにする")]
        public bool RememberMe { get; set; }
    }
}