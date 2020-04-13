using Netplanetes.CMS.Models.DomainModel;
using Netplanetes.CMS.Web.Areas.Admin.Models;
using Netplanetes.CMS.Web.Infrastracture;
using Netplanetes.CMS.Web.Models.Content;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Netplanetes.CMS.Web.Services
{
    public class ContentService : IContentService
    {
        IContentRepository _repository = null;
        ICategoryMappingService _categoryMapper = null;
        public ContentService(IContentRepository repository, ICategoryMappingService categoryMapper)
        {
            _repository = repository;
            _categoryMapper = categoryMapper;
        }
        /// <summary>
        /// フィルター条件を設定したQueryableオブジェクトに変換する
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected virtual IQueryable<Article> TranslateFilter(IContentRepository repository, ArticleSearchFilter filter)
        {
            var query = _repository.Articles;
            if (!string.IsNullOrEmpty(filter.CategoryLID))
            {
                var category = _categoryMapper.GetByLinkTitle(filter.CategoryLID);
                if (category != null)
                {
                    query = query.Where(x => x.CategoryID == category.ID);
                }
            }
            if (!string.IsNullOrEmpty(filter.Series))
            {
                query = query.Where(x => x.Series == filter.Series);
            }
            if (!filter.IsManageMode)
            {
                // 承認されかつ、リリース日が現時点より前の記事
                query = query.Where(x => x.ReleaseDate <= DateTime.Now && x.Approved == true);
                if (!filter.IncludeShowMemberOnly)
                {
                    // メンバー専用の記事を表示しない場合は、除外するように設定
                    query = query.Where(x => x.OnlyForMembers == false);
                }
                if (!filter.IncludeNonListed)
                {
                    // 一覧に表示が不許可のものを除外する場合は、一覧表示の許可が設定されているもののみ検索
                    query = query.Where(x => x.Listed == true);
                }
            }

            return query.AsQueryable();
        }

        public ArticleViewModel RetrieveArticleByLinkTitle(string linkTitle, bool includeMembersOnly)
        {
            var query = _repository.Articles.Where(x => x.LID == linkTitle && x.Approved == true && x.ReleaseDate <= DateTime.Now);
            if (includeMembersOnly)
            {
                query = query.Where(x => x.OnlyForMembers == false);
            }
            var article = query.FirstOrDefault();

            if (article == null) return null;

            ArticleViewModel model = new ArticleViewModel();
            model.NextArticleLID = _repository.Articles.Where(x => x.Approved && x.ReleaseDate <= DateTime.Now && x.ArticleID > article.ArticleID)
                .OrderBy(x => x.ArticleID)
                .Select(x => x.LID).FirstOrDefault();
            model.PreviousArticleLID = _repository.Articles.Where(x => x.Approved && x.ReleaseDate <= DateTime.Now && x.ArticleID < article.ArticleID)
                .OrderByDescending(x => x.ArticleID)
                .Select(x => x.LID).FirstOrDefault();

            model.InjectFrom(article);

            return model;
        }
        public ArticleSummaryListViewModel RetrieveArticleList(ArticleSearchFilter filter)
        {
            // フィルター条件準備
            var query = TranslateFilter(_repository, filter);
            // トータル件数カウント
            int count = query.Count();

            // 実レコード取得のためのフィルター条件を追加設定
            // ソート条件とページング
            query = query.OrderByDescending(x => x.ReleaseDate).ThenByDescending(x => x.UpdatedDate);
            query = query.Skip((filter.PageNum) * filter.PageSize).Take(filter.PageSize);

            ArticleSummaryListViewModel model = new ArticleSummaryListViewModel();
            if (!string.IsNullOrEmpty(filter.CategoryLID))
            {
                var category = _categoryMapper.GetByLinkTitle(filter.CategoryLID);

                if (category != null)
                {
                    model.CategoryDisplayTitle = category.DisplayTitle;
                    model.CategoryLID = category.LID;
                }
            }
            if (!string.IsNullOrEmpty(filter.Series))
            {
                model.Series = filter.Series;
            }
            model.Articles = CreateArticleListViewModels(query);
            model.PagingInfo = new Models.Shared.PagingInfo
            {
                CurrentPage = filter.PageNum,
                PageSize = filter.PageSize,
                TotalRecordCount = count
            };
            return model;
        }
        /// <summary>
        /// 抽出条件設定済みのQueryableなArticleに対して ArticleSummaryViewModel を取得する 
        /// </summary>
        /// <param name="articles"></param>
        /// <returns></returns>
        private IEnumerable<ArticleSummaryViewModel> CreateArticleListViewModels(IQueryable<Article> articles)
        {
            var query = from a in articles
                        select new ArticleSummaryViewModel
                        {
                            LID = a.LID,
                            DisplayTitle = a.DisplayTitle,
                            Abstract = a.Abstract,
                            ReleaseDate = a.ReleaseDate,
                            Approved = a.Approved,
                            TotalRating = a.TotalRating,
                            ViewCount = a.ViewCount,
                            CategoryTitle = a.Category.DisplayTitle,
                            CssClass = a.Category.CssClass,
                            CommentCount = a.Comments.Count()
                        };

            return query.ToList();
        }

        public bool IncrementViewCount(int articleId)
        {
            return _repository.IncrementArticleViewCount(articleId) == 1;
        }
        public async System.Threading.Tasks.Task<bool> IncrementViewCountAsync(int articleId)
        {
            return await _repository.IncrementArticleViewCountAsync(articleId) == 1;
        }
        /// <summary>
        /// ArticleId をもとに関連するコメントの一覧を取得する
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IEnumerable<CommentViewModel> RetrieveCommentList(int articleId)
        {
            var query = from c in _repository.Comments
                        where c.ArticleID == articleId
                        select c;

            List<CommentViewModel> models = new List<CommentViewModel>();
            foreach (var comment in query)
            {
                CommentViewModel model = new CommentViewModel();
                model.InjectFrom(comment);
                models.Add(model);
            }
            return models;

        }

        /// <summary>
        /// コメントを登録する
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="clientIp"></param>
        /// <returns></returns>
        public bool AddComment(CommentPostViewModel postedComment, string clientIp)
        {
            Comment comment = new Comment();
            comment.InjectFrom(postedComment);

            comment.AddedBy = HttpUtility.HtmlEncode(comment.AddedBy);
            comment.AddedByWeb = HttpUtility.HtmlEncode(comment.AddedByWeb);
            comment.AddedByIP = clientIp;
            comment.Body = HttpUtility.HtmlEncode(comment.Body);

            comment.UpdatedBy = comment.AddedBy;
            comment.AddedDate = DateTime.Now;
            comment.UpdatedDate = DateTime.Now;

            _repository.AddComment(comment);
            return _repository.SaveChanges() == 1;
        }


        public EditCategoryListViewModel RetrieveCategories()
        {
            EditCategoryListViewModel list = new EditCategoryListViewModel();
            var query = _repository.Categories.OrderBy(x => x.SortOrder);
            foreach (var item in query)
            {
                EditCategoryViewModel model = new EditCategoryViewModel();
                model.InjectFrom(item);
                list.Add(model);
            }
            return list;
        }
        public EditCategoryViewModel GetCategoryByLinkTitle(string title)
        {
            var category = _repository.Categories.Where(x => x.LID == title).FirstOrDefault();
            EditCategoryViewModel model = new EditCategoryViewModel();
            model.InjectFrom(category);

            return model;
        }


        public bool DeleteCategory(string title)
        {
            var category = _repository.Categories.Where(x => x.LID == title).FirstOrDefault();
            if (category != null)
            {
                _repository.DeleteCategory(category);
                _repository.SaveChanges();
            }
            return false;
        }
        public bool UpdateCategory(EditCategoryViewModel model, IIdentity identity)
        {
            var category = _repository.Categories.Where(x => x.CategoryID == model.CategoryID).FirstOrDefault();
            if (category != null)
            {
                IgnoreSupportSameNameTypeInjection<Category> injection = new IgnoreSupportSameNameTypeInjection<Category>(
                   x => x.AddedBy,
                   x => x.AddedDate,
                   x => x.CategoryID
               );
                category.InjectFrom(injection, model);
                category.UpdatedDate = DateTime.Now;
                category.UpdatedBy = identity.Name;
                if (string.IsNullOrEmpty(category.LID))
                {
                    category.LID = category.DisplayTitle;
                }
            }
            return _repository.SaveChanges() == 1;
        }
        public bool CreateCategory(EditCategoryViewModel model, IIdentity identity)
        {
            var category = new Category();
            category.InjectFrom(model);
            category.AddedBy = identity.Name;
            category.AddedDate = DateTime.Now;
            category.UpdatedBy = identity.Name;
            category.UpdatedDate = DateTime.Now;
            if (string.IsNullOrEmpty(category.LID))
            {
                category.LID = category.DisplayTitle.Replace(" ", "-");
            }

            _repository.AddCategory(category);
            return _repository.SaveChanges() == 1;
        }



        public EditCommentListViewModel RetrieveComments(int pageNum = 0, int pageSize = 10)
        {
            var count = _repository.Comments.Count();
            var query = from c in _repository.Comments
                        orderby c.UpdatedDate descending
                        select new
                        {
                            CommentID = c.CommentID,
                            Body = c.Body,
                            AddedBy = c.AddedBy,
                            AddedByEmail = c.AddedByEmail,
                            AddedByWeb = c.AddedByWeb,
                            AddedByIP = c.AddedByIP,
                            AddedDate = c.AddedDate,
                            ArticleLID = c.Article.LID
                        };
            query.Skip(pageNum * pageSize).Take(pageSize);

            List<EditCommentViewModel> list = new List<EditCommentViewModel>();
            foreach (var item in query)
            {
                EditCommentViewModel commentModel = new EditCommentViewModel();
                commentModel.InjectFrom(item);
                list.Add(commentModel);
            }
            EditCommentListViewModel model = new EditCommentListViewModel();
            model.Comments = list;
            model.PagingInfo = new Models.Shared.PagingInfo()
            {
                CurrentPage = pageNum,
                PageSize = pageSize,
                TotalRecordCount = count
            };
            return model;
        }

        public bool DeleteComment(int commentId)
        {
            var comment = _repository.Comments.Where(x => x.CommentID == commentId).FirstOrDefault();
            if (comment != null)
            {
                _repository.DeleteComment(comment);
                return _repository.SaveChanges() == 1;
            }
            return false;
        }

        public bool UpdateComment(EditCommentViewModel model, IIdentity identity)
        {
            var comment = _repository.Comments.Where(x => x.CommentID == model.CommentID).FirstOrDefault();
            if (comment != null)
            {
                IgnoreSupportSameNameTypeInjection<Comment> injection = new IgnoreSupportSameNameTypeInjection<Comment>(
                   x => x.AddedBy,
                   x => x.AddedDate,
                   x => x.CommentID,
                   x => x.ArticleID,
                   x => x.AddedByIP
               );
                comment.InjectFrom(injection, model);
                comment.UpdatedDate = DateTime.Now;
                comment.UpdatedBy = identity.Name;
            }
            return _repository.SaveChanges() == 1;

        }

        public EditCommentViewModel GetCommentById(int commentId)
        {
            var query = from c in _repository.Comments
                        where c.CommentID == commentId
                        orderby c.UpdatedDate descending
                        select new
                        {
                            CommentID = c.CommentID,
                            Body = c.Body,
                            AddedBy = c.AddedBy,
                            AddedByEmail = c.AddedByEmail,
                            AddedByWeb = c.AddedByWeb,
                            AddedByIP = c.AddedByIP,
                            AddedDate = c.AddedDate,
                            ArticleLID = c.Article.LID
                        };
            var comments = query.ToList();
            if (comments.Count > 0)
            {
                EditCommentViewModel model = new EditCommentViewModel();
                model.InjectFrom(comments[0]);
                return model;
            }
            return null;
        }

        public EditArticleViewModel GetArticleByLinkTitle(string linkTitle)
        {
            var article = _repository.Articles.Where(x => x.LID == linkTitle).FirstOrDefault();
            if (article == null) return null;

            var model = new EditArticleViewModel();
            model.InjectFrom(article);

            return model;
        }
        public bool DeleteArticle(string title)
        {
            var article = _repository.Articles.Where(x => x.LID == title).FirstOrDefault();
            if (article != null)
            {
                _repository.DeleteArticle(article);
                return _repository.SaveChanges() == 1;
            }
            return false;
        }

        public bool UpdateArticle(EditArticleViewModel model, IIdentity identity)
        {
            if (string.IsNullOrEmpty(model.LID))
            {
                model.LID = model.DisplayTitle;
            }
            model.LID = SanitizeLID(model.LID);

            var article = _repository.Articles.Where(x => x.ArticleID == model.ArticleID).FirstOrDefault();
            if (article != null)
            {
                IgnoreSupportSameNameTypeInjection<Article> injection = new IgnoreSupportSameNameTypeInjection<Article>(
                   x => x.AddedBy,
                   x => x.AddedDate,
                   x => x.ArticleID
                );

                article.InjectFrom(injection, model);
                article.UpdatedDate = DateTime.Now;
                article.UpdatedBy = identity.Name;
                return _repository.SaveChanges() == 1;
            }
            return false;
        }

        /// <summary>
        /// アーティクルを作成する
        /// </summary>
        /// <param name="model"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public bool CreateArticle(EditArticleViewModel model, IIdentity identity)
        {
            var article = new Article();
            article.InjectFrom(model);
            article.AddedDate = DateTime.Today;
            article.UpdatedDate = DateTime.Today;
            article.AddedBy = identity.Name;
            article.UpdatedBy = identity.Name;
            article.ViewCount = 0;
            article.TotalRating = 0;
            article.Votes = 0;
            if (string.IsNullOrEmpty(article.LID))
            {
                article.LID = article.DisplayTitle;
            }
            article.LID = SanitizeLID(article.LID);

            _repository.AddArticle(article);
            return _repository.SaveChanges() == 1;
        }
        private string SanitizeLID(string lid)
        {
            var regex = new System.Text.RegularExpressions.Regex("[\":<>&%*\\.\"+]");
            lid = lid.TrimEnd(' ').TrimStart(' ').Replace(' ', '-').Replace(":", "");
            return regex.Replace(lid, "");
        }
        public IEnumerable<string> RetrieveSeriesTags()
        {
            return _repository.Articles.Where(x => x.Series != null).Select(x => x.Series).Distinct();
        }
        public EditFavoriteLinkListViewModel RetrieveFavoriteLinks()
        {
            var query = _repository.FavoriteLinks.OrderBy(x=> x.SortOrder)
                .Select(x => new EditFavoriteLinkViewModel
                {
                    FavoriteLinkID = x.FavoriteLinkID,
                    Text = x.Text,
                    Url = x.Url,
                    SortOrder = x.SortOrder
            });

            var model =  new EditFavoriteLinkListViewModel();
            model.AddRange(query.ToList());

            return model;
        }

        public bool DeleteFavoriteLink(int linkid)
        {
            var favorite = _repository.FavoriteLinks.Where(x => x.FavoriteLinkID == linkid).FirstOrDefault();
            if (favorite == null)
            {
                return false;
            }
            _repository.DeleteFavoriteLink(favorite);
            return _repository.SaveChanges() == 1;


        }
        public bool UpdateFavoriteLink(EditFavoriteLinkViewModel model, IIdentity identity)
        {
            var favorite = _repository.FavoriteLinks.Where(x => x.FavoriteLinkID == model.FavoriteLinkID).FirstOrDefault();
            if(favorite == null){
                return false;
            }
            IgnoreSupportSameNameTypeInjection<FavoriteLink> injection = new IgnoreSupportSameNameTypeInjection<FavoriteLink>(
                x => x.AddedBy,
                x => x.AddedDate,
                x => x.FavoriteLinkID
               );
            favorite.InjectFrom(model);
            favorite.UpdatedBy = identity.Name;
            favorite.UpdatedDate = DateTime.Now;

            return _repository.SaveChanges() == 1;
        }

        public EditFavoriteLinkViewModel GetFavoriteLinkById(int linkId)
        {
            var favorite = _repository.FavoriteLinks.Where(x => x.FavoriteLinkID == linkId).FirstOrDefault();

            if (favorite == null)
            {
                return null;
            }
            EditFavoriteLinkViewModel model = new EditFavoriteLinkViewModel();
            model.InjectFrom(favorite);

            return model;

        }


        public bool CreateFavoriteLink(EditFavoriteLinkViewModel model, IIdentity identity)
        {
            FavoriteLink link = new FavoriteLink();
            link.InjectFrom(model);
            link.AddedBy = identity.Name;
            link.UpdatedBy = identity.Name;
            link.AddedDate = DateTime.Now;
            link.UpdatedDate = DateTime.Now;

            _repository.AddFavoriteLink(link);
            return _repository.SaveChanges() == 1;
        }
    }

}