using Netplanetes.CMS.Models.DomainModel;
using Netplanetes.CMS.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web.Services
{
    /// <summary>
    /// カテゴリーIDとカテゴリータイトルのマッピングを提供するサービス
    /// </summary>
    public class CategoryMappingService : ICategoryMappingService
    {
        /// <summary>
        /// コンテンツリポジトリ
        /// </summary>
        private IContentRepository _repository;
        /// <summary>
        /// LinkTitleをキーとする CategoryのActionLinkVieModel ディクショナリ
        /// </summary>
        IDictionary<string, ActionLinkValueViewModel> _linktTitleMap;
        /// <summary>
        /// CategoryIDをキーとする CategoryのActionLinkViewModel ディクショナリ
        /// </summary>
        IDictionary<int, ActionLinkValueViewModel> _categoryIdMap;

        public CategoryMappingService(IContentRepository repository)
        {
            _repository = repository;
            BuildMap();
        }

        public void BuildMap()
        {
            _linktTitleMap = new Dictionary<string, ActionLinkValueViewModel>();
            _categoryIdMap = new Dictionary<int, ActionLinkValueViewModel>();

            IList<ActionLinkValueViewModel> list = new List<ActionLinkValueViewModel>();

            var query = from c in _repository.Categories
                        orderby c.SortOrder
                        select new ActionLinkValueViewModel{
                            ID = c.CategoryID,
                            LID = c.LID,
                            DisplayTitle = c.DisplayTitle,
                            CssClass = c.CssClass,
                            Description = c.ShortDescription,
                            Value = c.Articles.Count().ToString()
                        };
           
            query.ToList().ForEach(x =>
            {
                _linktTitleMap.Add(x.LID, x);
                _categoryIdMap.Add(x.ID, x);
            });
        }

        public ActionLinkViewModel GetByLinkTitle(string linkTitle)
        {
            return _linktTitleMap[linkTitle];
        }

        public ActionLinkViewModel GetByCategoryId(int categoryId)
        {
            return _categoryIdMap[categoryId];
        }
        public IReadOnlyCollection<ActionLinkViewModel> CategoriesLinks
        {
            get
            {
                return _linktTitleMap.Values.ToList().AsReadOnly();
            }
        }
        public IReadOnlyCollection<ActionLinkValueViewModel> CategoriesWithArticleCountLinks
        {
            get
            {
                return _linktTitleMap.Values.ToList().AsReadOnly();
            }
        }
    }
}