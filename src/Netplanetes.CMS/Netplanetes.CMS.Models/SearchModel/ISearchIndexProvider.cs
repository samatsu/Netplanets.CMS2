using Netplanetes.CMS.Models.DomainModel;
using System;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.SearchModel
{
    public interface ISearchIndexProvider
    {
        Task CreateIndex();
        Task DeleteIndex();
        Task UploadIndex(IContentRepository repository, DateTime updateSince);
        SearchResponse<ArticleIndex> Search(string searchText, int next);
        void Initialize();
    }
}