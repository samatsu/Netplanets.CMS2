using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Netplanetes.CMS.Models.DomainModel;
using Netplanetes.CMS.Models.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.UpdateSearchIndex
{
    class IndexProcessor
    {
        #region コンストラクタ
        public IndexProcessor()
        {
        }
        #endregion

        #region properties
        private IndexOption _option;
        private ISearchIndexProvider _indexProvider;
        #endregion
        public async Task ProcessIndexing(IndexOption option)
        {
            _option = option;
            _indexProvider = CreateIndexProvider();

            if (_option.Operation == Mode.Rebuild)
            {
                await RecreateIndex();
                await UploadIndex(DateTime.MinValue);
            }
            else
            {
                await UploadIndex(_option.UpdateSince);
            }
        }
        protected virtual async Task RecreateIndex()
        {
            // インデックスをドロップ
            Console.WriteLine("インデックス定義を削除します");
            await _indexProvider.DeleteIndex();
            Console.WriteLine("インデックス定義を作成します");
            await _indexProvider.CreateIndex();
        }
        protected virtual async Task UploadIndex(DateTime updateSince)
        {
            Console.WriteLine(string.Format("{0}以降に更新された記事のインデックスをアップロードします", updateSince));

            IContentRepository repository = CreateRepository();
            await _indexProvider.UploadIndex(repository, updateSince);
        }


        private IContentRepository CreateRepository()
        {
            return new EFCFContentRepository();
        }

        private ISearchIndexProvider CreateIndexProvider()
        {
            return new AzureSearchIndexProvider();
        }
    }
}
