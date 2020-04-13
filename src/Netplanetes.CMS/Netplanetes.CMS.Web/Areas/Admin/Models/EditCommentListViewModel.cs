using Netplanetes.CMS.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Areas.Admin.Models
{
    public class EditCommentListViewModel : List<EditCommentViewModel>
    {
        /// <summary>
        /// コメントリスト
        /// </summary>
        public IEnumerable<EditCommentViewModel> Comments { get; set; }

        /// <summary>
        /// ページング情報
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

    }
}