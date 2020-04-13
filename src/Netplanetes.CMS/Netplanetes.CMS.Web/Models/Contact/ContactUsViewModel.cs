using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Models.Contact
{
    /// <summary>
    /// コンタクトフォーム用のビューモデル
    /// </summary>
    public class ContactUsViewModel
    {
        /// <summary>
        /// 送信者名
        /// </summary>
        [DisplayName("お名前")]
        [Required(ErrorMessage = "お名前を入力してください.")]
        [StringLength(255)]
        public string Name { get; set; }
        /// <summary>
        /// メールアドレス
        /// </summary>
        [DisplayName("メールアドレス")]
        [Required(ErrorMessage = "メールアドレスを入力してください.")]
        [StringLength(255)]
        [EmailAddress(ErrorMessage="メールアドレスの形式が不正です")]
        //[RegularExpression("\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "メールアドレスが不正です.")]
        //[RegularExpression("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$", ErrorMessage = "メールアドレスの形式が不正です.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        /// <summary>
        /// メール件名
        /// </summary>
        [DisplayName("タイトル")]
        [Required(ErrorMessage = "お問い合わせのタイトルを入力してください.")]
        [StringLength(255)]
        public string Subject { get; set; }
        /// <summary>
        /// メール本文
        /// </summary>
        [DisplayName("お問い合わせの内容")]
        [Required(ErrorMessage = "お問い合わせ内容を入力して下さい.")]
        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }
}