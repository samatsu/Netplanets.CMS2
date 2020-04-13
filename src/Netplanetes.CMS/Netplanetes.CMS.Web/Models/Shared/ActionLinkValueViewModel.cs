using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Models.Shared
{
    /// <summary>
    /// リンク情報に追加の情報(Valueプロパティ) を持つクラス
    /// </summary>
    public class ActionLinkValueViewModel : ActionLinkViewModel
    {
        public ActionLinkValueViewModel()
        {

        }
        public ActionLinkValueViewModel(int id, string lid, string displayTitle, string cssClass, string description, string value)
            : base(id, lid, displayTitle, cssClass)
        {
            this.Value = value;
            this.Description = description;
        }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}