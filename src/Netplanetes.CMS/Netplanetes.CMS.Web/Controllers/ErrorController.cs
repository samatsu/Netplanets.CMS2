using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Generic()
        {
            return View("HandleError");
        }
        public ViewResult NotFound()
        {
            return View("_404Error", (object)"404 Page Not Found - ページが見つかりませんでした.");
        }
    }
}