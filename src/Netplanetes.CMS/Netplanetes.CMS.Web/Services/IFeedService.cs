using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Services
{
    public interface IFeedService
    {
        string CreateSiteMap(UrlHelper helper);
        string CreateRss20(UrlHelper helper);
        string CreateAtom10(UrlHelper helper);
    }
}
