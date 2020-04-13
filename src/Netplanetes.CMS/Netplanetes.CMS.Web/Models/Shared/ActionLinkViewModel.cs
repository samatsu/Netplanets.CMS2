using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Models.Shared
{
    public class ActionLinkViewModel
    {
        public ActionLinkViewModel() : this(-1, "", "", "fa fa-link") { }
        public ActionLinkViewModel(int id, string lid, string displayTitle, string cssClass)
        {
            this.ID = id;
            this.LID = lid;
            this.DisplayTitle = displayTitle;
            this.CssClass = cssClass;
        }

        private int _id = -1;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _linkId = null;
        public string LID
        {
            get { return _linkId; }
            set { _linkId = value; }
        }
        private string _displayTitle = null;
        public string DisplayTitle
        {
            get { return _displayTitle; }
            set { _displayTitle = value; }
        }
        public string CssClass
        {
            get;
            set;
        }
    }
}