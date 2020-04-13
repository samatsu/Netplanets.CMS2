using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Web.Services
{
    public interface ISearchService
    {
        Models.Search.SearchResultViewModel Search(string searchText, int page);
    }
}
