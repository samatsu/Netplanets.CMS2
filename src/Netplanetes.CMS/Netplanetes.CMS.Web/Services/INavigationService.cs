using Netplanetes.CMS.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Web.Services
{
    public interface INavigationService
    {
        IEnumerable<ActionLinkViewModel> RetrieveCatetoriesLinks();
        IEnumerable<ActionLinkValueViewModel> RetrieveCategoriesLinksWithArticleCount();
        IEnumerable<ActionLinkViewModel> RetrieveLatestArticlesLinks(int count = 5);
        IEnumerable<GeneralLinkViewModel> RetrieveFavoriteLinks();
    }
}
