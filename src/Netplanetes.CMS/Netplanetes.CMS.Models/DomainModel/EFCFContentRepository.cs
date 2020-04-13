using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.DomainModel
{
    public class EFCFContentRepository : IContentRepository
    {
        private ContentDbContext _context = new ContentDbContext();

        public IQueryable<Category> Categories
        {
            get { return _context.Categories; }
        }

        public IQueryable<Article> Articles
        {
            get { return _context.Articles; }
        }

        public IQueryable<Comment> Comments
        {
            get { return _context.Comments; }
        }

        public IQueryable<FavoriteLink> FavoriteLinks
        {
            get { return _context.FavoriteLinks; }
        }


        public int IncrementArticleViewCount(int articleId)
        {
            return _context.Database.ExecuteSqlCommand("update [dbo].[Article] set ViewCount = ViewCount + 1 where ArticleID = @p0", articleId);
        }
        public async Task<int> IncrementArticleViewCountAsync(int articleId)
        {
            return await _context.Database.ExecuteSqlCommandAsync("update [dbo].[Article] set ViewCount = ViewCount + 1 where ArticleID = @p0", articleId);
        }

        public int VoteArticle(int articleId, int rating)
        {
            return _context.Database.ExecuteSqlCommand("update [dbo].[Article] set TotalRating = TotalRating + @p1,Votes = Votes + 1 where ArticleID = @p0", articleId, rating);
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public void AddArticle(Article article)
        {
            _context.Articles.Add(article);
        }

        public void DeleteArticle(Article article)
        {
            _context.Articles.Remove(article);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
        }


        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var ve in ex.EntityValidationErrors)
                {
                    foreach (var error in ve.ValidationErrors)
                    {
                        System.Diagnostics.Trace.Write(error.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void AddFavoriteLink(FavoriteLink favoriteLink)
        {
            _context.FavoriteLinks.Add(favoriteLink);
        }

        public void DeleteFavoriteLink(FavoriteLink favoriteLink)
        {
            _context.FavoriteLinks.Remove(favoriteLink);
        }

    }
}
