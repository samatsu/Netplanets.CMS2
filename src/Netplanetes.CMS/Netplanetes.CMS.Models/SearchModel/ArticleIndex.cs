using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.SearchModel
{
    [SerializePropertyNamesAsCamelCase]
    public class ArticleIndex
    {
        public string Id { get; set; }
        public string Lid { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public bool? MemberOnly { get; set; }
        public string Approved { get; set; }
        public string Category { get; set; }
        public string SeriesTag { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset ReleasedOn { get; set; }

        public static ArticleIndex Create(DomainModel.Article model)
        {
            var article = new ArticleIndex
            {
                Id = model.ArticleID.ToString(),
                Lid = model.LID,
                Title = model.DisplayTitle,
                Summary = model.Abstract,
                Content = model.Body,
                MemberOnly = model.OnlyForMembers,
                Approved = model.Approved ? "1" : "0",
                Category = model.Category.DisplayTitle,
                SeriesTag = model.Series,
                CreatedBy = model.AddedBy,
                ReleasedOn = new DateTimeOffset(model.ReleaseDate)
            };

            return article;
        }
    }
}
