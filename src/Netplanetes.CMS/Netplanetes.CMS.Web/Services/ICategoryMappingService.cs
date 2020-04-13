using Netplanetes.CMS.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Web.Services
{
    public interface ICategoryMappingService
    {
        ActionLinkViewModel GetByLinkTitle(string linkTitle);

        ActionLinkViewModel GetByCategoryId(int categoryId);

        IReadOnlyCollection<ActionLinkViewModel> CategoriesLinks { get; }
        IReadOnlyCollection<ActionLinkValueViewModel> CategoriesWithArticleCountLinks { get; }
    }
}
