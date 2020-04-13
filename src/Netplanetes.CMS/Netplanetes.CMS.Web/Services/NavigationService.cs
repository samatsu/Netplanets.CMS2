using Netplanetes.CMS.Models.DomainModel;
using Netplanetes.CMS.Web.Models.Shared;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Netplanetes.CMS.Web.Services
{
    public class NavigationService : INavigationService
    {
        IContentRepository _repository;
        ICategoryMappingService _categoryMapping;
        public NavigationService(IContentRepository repository, ICategoryMappingService categoryMapping)
        {
            _repository = repository;
            _categoryMapping = categoryMapping;
        }

        public IEnumerable<ActionLinkViewModel> RetrieveCatetoriesLinks()
        {
            return _categoryMapping.CategoriesLinks;
        }

        public IEnumerable<ActionLinkValueViewModel> RetrieveCategoriesLinksWithArticleCount()
        {
            return _categoryMapping.CategoriesWithArticleCountLinks;
            //IList<ActionLinkValueViewModel> list = new List<ActionLinkValueViewModel>();
            //var query = _repository.Categories.Select(x => new { ID = x.CategoryID, LinkTitle = x.LID, DisplayTitle = x.DisplayTitle, CssClass = x.CssClass, ShortDesciption = x.ShortDescription, Value = x.Articles.Count() });

            //return query.ToList().Select(x => new ActionLinkValueViewModel(x.ID, x.LinkTitle, x.DisplayTitle, x.CssClass,  x.ShortDesciption ,x.Value.ToString()));
        }

        public IEnumerable<ActionLinkViewModel> RetrieveLatestArticlesLinks(int count = 5)
        {
            IList<ActionLinkViewModel> list = new List<ActionLinkViewModel>();
            var query = from a in _repository.Articles
                        where a.Approved == true && a.ReleaseDate <= DateTime.Now
                        orderby a.UpdatedDate descending
                        select new {
                            ArticleID = a.ArticleID,
                            LID = a.LID,
                            DisplayTitle = a.DisplayTitle,
                            CssClass = a.Category.CssClass
                        };

            return query.Take(count).Select(x => new ActionLinkViewModel
            {
                ID = x.ArticleID,
                LID = x.LID,
                DisplayTitle = x.DisplayTitle,
                CssClass = x.CssClass
            }).ToList();

        }

        public IEnumerable<GeneralLinkViewModel> RetrieveFavoriteLinks()
        {
            IList<GeneralLinkViewModel> list = new List<GeneralLinkViewModel>();
            var query = from f in _repository.FavoriteLinks
                        orderby f.SortOrder
                        select new GeneralLinkViewModel
                        {
                            Url = f.Url,
                            Text = f.Text
                        };
            return query.ToList();
        }
    }
}