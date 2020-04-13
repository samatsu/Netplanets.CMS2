using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.SearchModel
{
    public class SearchResponse<T> where T : class
    {
        public long Count { get; set; }
        public IEnumerable<SearchResultDocument<T>> Documents { get; set; }
    }
    public class SearchResultDocument<T> where T : class
    {
        public T Document { get; set; }
        public string HighLight { get; set; }
    }
}
