using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.DomainModel
{
    public interface IContentRepository
    {
        IQueryable<Category> Categories { get; }
        IQueryable<Article> Articles { get; }
        IQueryable<Comment> Comments { get; }
        IQueryable<FavoriteLink> FavoriteLinks { get; }
        /// <summary>
        /// 記事のビューカウントをカウントアップする
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        int IncrementArticleViewCount(int articleId);
        /// <summary>
        /// 記事のビューカウントをカウントアップする
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task<int> IncrementArticleViewCountAsync(int articleId);
        /// <summary>
        /// コメントを追加する
        /// </summary>
        /// <param name="comment"></param>
        void AddComment(Comment comment);
        /// <summary>
        /// コメントを削除する
        /// </summary>
        /// <param name="comment"></param>
        void DeleteComment(Comment comment);
        /// <summary>
        /// 記事を追加する
        /// </summary>
        /// <param name="article"></param>
        void AddArticle(Article article);
        /// <summary>
        /// 記事を削除する
        /// </summary>
        /// <param name="article"></param>
        void DeleteArticle(Article article);
        /// <summary>
        /// カテゴリを追加する
        /// </summary>
        /// <param name="category"></param>
        void AddCategory(Category category);
        /// <summary>
        /// カテゴリを削除する
        /// </summary>
        /// <param name="category"></param>
        void DeleteCategory(Category category);
        /// <summary>
        /// お気に入りリンクを追加する
        /// </summary>
        /// <param name="favoriteLink"></param>
        void AddFavoriteLink(FavoriteLink favoriteLink);
        /// <summary>
        /// お気に入りリンクを削除する
        /// </summary>
        /// <param name="favoriteLink"></param>
        void DeleteFavoriteLink(FavoriteLink favoriteLink);
        /// <summary>
        /// 記事に対して投票を行う
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        int VoteArticle(int articleId, int rating);
        int SaveChanges();

    }
}
