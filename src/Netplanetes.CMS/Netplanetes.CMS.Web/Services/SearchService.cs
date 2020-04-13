using Netplanetes.CMS.Models.SearchModel;
using Netplanetes.CMS.Web.Models.Content;
using Netplanetes.CMS.Web.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Services
{
    public class SearchService : ISearchService
    {
        private ISearchIndexProvider _provider;
        public SearchService(ISearchIndexProvider provider)
        {
            _provider = provider;
        }

        public SearchResultViewModel Search(string searchText, int page = 0)
        {
            var response = _provider.Search(searchText, page * 10);
            SearchResultViewModel model = new SearchResultViewModel();
            model.PagingInfo = new Models.Shared.PagingInfo
            {
                CurrentPage = page,
                PageSize = 10,
                TotalRecordCount = (int)response.Count
            };
            model.Query = searchText;
            model.Articles = Read(response.Documents);
            return model;
        }
        public IEnumerable<ArticleSummaryViewModel> Read(IEnumerable<SearchResultDocument<ArticleIndex>> docs)
        {
            IList<ArticleSummaryViewModel> list = new List<ArticleSummaryViewModel>();
            foreach (var item in docs)
            {
                var doc = item.Document;
                var summary = !string.IsNullOrEmpty(item.HighLight) ? item.HighLight : doc.Summary;
                var model = new ArticleSummaryViewModel
                {
                    LID = doc.Lid,
                    DisplayTitle = doc.Title,
                    Abstract = summary,
                    ReleaseDate = doc.ReleasedOn.DateTime,
                    Approved = doc.Approved == "1",
                    CategoryTitle = doc.Category
                };
                list.Add(model);
            }
            return list;
        }

    }
}