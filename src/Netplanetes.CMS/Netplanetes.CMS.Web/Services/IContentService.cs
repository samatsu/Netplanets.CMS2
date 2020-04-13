using Netplanetes.CMS.Web.Areas.Admin.Models;
using Netplanetes.CMS.Web.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Web.Services
{
    public interface IContentService
    {
        #region パブリックAPI
        ArticleSummaryListViewModel RetrieveArticleList(ArticleSearchFilter filter);
        ArticleViewModel RetrieveArticleByLinkTitle(string linkTitle, bool includeMembersOnly);
        IEnumerable<CommentViewModel> RetrieveCommentList(int articleId);
        bool AddComment(CommentPostViewModel postedComment, string clientIp);
        System.Threading.Tasks.Task<bool> IncrementViewCountAsync(int articleId);
        #endregion

        #region カテゴリ管理API
        EditCategoryListViewModel RetrieveCategories();
        bool DeleteCategory(string title);
        bool UpdateCategory(EditCategoryViewModel model, IIdentity identity);
        bool CreateCategory(EditCategoryViewModel model, IIdentity identity);
        EditCategoryViewModel GetCategoryByLinkTitle(string title);
        #endregion
        #region 記事管理API
        EditArticleViewModel GetArticleByLinkTitle(string linkTitle);
        bool DeleteArticle(string title);
        bool UpdateArticle(EditArticleViewModel model, IIdentity identity);
        bool CreateArticle(EditArticleViewModel model, IIdentity identity);
        IEnumerable<string> RetrieveSeriesTags();
        #endregion
        #region コメント管理API
        EditCommentListViewModel RetrieveComments(int pageNum, int pageSize);
        bool DeleteComment(int commentId);
        bool UpdateComment(EditCommentViewModel model, IIdentity identity);
        EditCommentViewModel GetCommentById(int commentId);
        #endregion
        #region お気に入り管理API
        EditFavoriteLinkListViewModel RetrieveFavoriteLinks();
        bool DeleteFavoriteLink(int linkId);
        bool UpdateFavoriteLink(EditFavoriteLinkViewModel model, IIdentity identity);
        EditFavoriteLinkViewModel GetFavoriteLinkById(int linkId);
        bool CreateFavoriteLink(EditFavoriteLinkViewModel model, IIdentity identity);
        #endregion
    }
}
