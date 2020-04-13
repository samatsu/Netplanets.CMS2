using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Netplanetes.CMS.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Netplanetes.CMS.Models.SearchModel
{
    public class AzureSearchIndexProvider : ISearchIndexProvider, IDisposable
    {
        #region constructor
        public AzureSearchIndexProvider()
        {
            Initialize();
        }
        #endregion

        #region properties
        private SearchServiceClient _searchClient;
        private ISearchIndexClient _indexClient;
        public string SearchServiceApiKey
        {
            get { return ConfigurationManager.AppSettings["SearchServiceApiKey"]; }
        }
        public string SearchServiceName
        {
            get { return ConfigurationManager.AppSettings["SearchServiceName"]; }
        }
        public string SearchIndex
        {
            get { return ConfigurationManager.AppSettings["SearchIndexName"]; }
        }
        public int UpdateBatchSize
        {
            get { return 100; }
        }
        #endregion

        public void Initialize()
        {
            // add for Azure Seach SockeException  under .net 4.5.2
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

            if (_searchClient != null)
            {
                _searchClient.Dispose();
            }
            if (_indexClient != null)
            {
                _indexClient.Dispose();
            }

            // 検索クライアント初期化
            _searchClient = new SearchServiceClient(SearchServiceName, new SearchCredentials(SearchServiceApiKey));
            _indexClient = _searchClient.Indexes.GetClient(SearchIndex);

        }
        /// <summary>
        /// create index definition
        /// </summary>
        /// <returns></returns>
        public async Task CreateIndex()
        {
            var definition = new Index()
            {
                Name = SearchIndex,
                Fields = new[]
                {
                        new Field("id",         DataType.String)         { IsKey = true,  IsSearchable = true,  IsFilterable = true, IsSortable = true, IsFacetable = false, IsRetrievable = true},
                        new Field("lid",        DataType.String)         { IsKey = false, IsSearchable = true,  IsFilterable = false,IsSortable = false,IsFacetable = false, IsRetrievable = true},
                        new Field("title",      DataType.String)         { IsKey = false, IsSearchable = true,  IsFilterable = true, IsSortable = true, IsFacetable = false, IsRetrievable = true},
                        new Field("summary",    DataType.String)         { IsKey = false, IsSearchable = true,  IsFilterable = true, IsSortable = false,IsFacetable = false, IsRetrievable = true},
                        new Field("content",    DataType.String)         { IsKey = false, IsSearchable = true,  IsFilterable = true, IsSortable = false,IsFacetable = false, IsRetrievable = false},
                        new Field("memberOnly", DataType.Boolean)        { IsKey = false, IsSearchable = false, IsFilterable = true, IsSortable = false,IsFacetable = false, IsRetrievable = true},
                        new Field("approved",   DataType.String)         { IsKey = false, IsSearchable = false, IsFilterable = true, IsSortable = false,IsFacetable = false, IsRetrievable = true},
                        new Field("category",   DataType.String)         { IsKey = false, IsSearchable = true,  IsFilterable = true, IsSortable = true, IsFacetable = true,  IsRetrievable = true},
                        new Field("seriesTag",  DataType.String)         { IsKey = false, IsSearchable = true,  IsFilterable = true, IsSortable = true, IsFacetable = true,  IsRetrievable = true},
                        new Field("createdBy",  DataType.String)         { IsKey = false, IsSearchable = true,  IsFilterable = true, IsSortable = true, IsFacetable = true,  IsRetrievable = true},
                        new Field("releasedOn",  DataType.DateTimeOffset){ IsKey = false, IsSearchable = false, IsFilterable = true, IsSortable = true, IsFacetable = true,  IsRetrievable = true},
                    },
                Suggesters = new[] {
                        new Suggester("suggestionByTitle", "title", "summary" )
                }
            };
            await _searchClient.Indexes.CreateAsync(definition);
        }


        public async Task UploadIndex(IContentRepository repository, DateTime updateSince)
        {
            int total = repository.Articles.Where(x => x.UpdatedDate > updateSince).Count();
            int next = 0;
            while (next < total)
            {
                var articles = repository.Articles.Where(x => x.UpdatedDate > updateSince).Include(x => x.Category).OrderBy(x => x.UpdatedDate).Skip(next).Take(UpdateBatchSize);
                IList<IndexAction<ArticleIndex>> actions = new List<IndexAction<ArticleIndex>>();
                foreach (var article in articles)
                {
                    actions.Add(
                         new IndexAction<ArticleIndex>()
                         {
                             ActionType = IndexActionType.Upload,
                             Document = ArticleIndex.Create(article)
                         }
                    );
                }
                var result = await _indexClient.Documents.IndexAsync(new IndexBatch<ArticleIndex>(actions));
                next += UpdateBatchSize;
            }
        }

        /// <summary>
        /// delete index definition
        /// </summary>
        /// <returns></returns>
        public async Task DeleteIndex()
        {
            await _searchClient.Indexes.DeleteAsync(SearchIndex);
        }

        public SearchResponse<ArticleIndex> Search(string searchText, int next)
        {

            SearchParameters sp = new SearchParameters() { SearchMode = SearchMode.All, Top = 10, IncludeTotalResultCount = true, Skip = next };
            sp.HighlightFields = new[] { "summary" };
            sp.HighlightPreTag = "<b>";
            sp.HighlightPostTag = "</b>";
            var response = _indexClient.Documents.Search<ArticleIndex>(searchText, sp);
            var result = new SearchResponse<ArticleIndex>
            {
                Count = response.Count.HasValue ? response.Count.Value : 0,
                Documents = response.Results.Select(x => new SearchResultDocument<ArticleIndex>
                { Document = x.Document, HighLight = ConcatString(x.Highlights, "summary") })
            };
            return result;
        }
        private string ConcatString(IDictionary<string, System.Collections.Generic.IList<string>> highlights, string key)
        {
            if (highlights == null) return null;
            if (highlights.ContainsKey(key))
            {
                IEnumerable<string> src = highlights[key];
                StringBuilder builder = new StringBuilder();
                foreach (var item in src)
                {
                    builder.Append(item);
                }
                return builder.ToString();
            }
            return null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_searchClient != null) _searchClient.Dispose();
                    if (_indexClient != null) _indexClient.Dispose();
                    _searchClient = null;
                    _indexClient = null;
                }

                disposedValue = true;
            }
        }


        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
        }
        #endregion
    }
}
