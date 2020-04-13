﻿using Netplanetes.CMS.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web
{
    public class InitializeData
    {
        public static void Init()
        {
            EFCFContentRepository repository = new EFCFContentRepository();
            // カテゴリーの作成
            Category category1 = CreateCategory("Desktop", "デスクトップアプリ全般");
            repository.AddCategory(category1);

            Category category2 = CreateCategory("Web", "Web アプリケーション情報全般");
            repository.AddCategory(category2);

            Category category3 = CreateCategory("Universal App", "ユニバーサルアプリケーション");
            repository.AddCategory(category3);

            Category category4 = CreateCategory("Windows", "Windows 全般情報");
            repository.AddCategory(category4);

            repository.SaveChanges();

            // コメントデータの作成
            Article article11 = CreateArticle(category1.CategoryID, "TSQL Server 2012]インデックス再構築タスクで、オンラインインデックス再構築できるのにオフラインインデックス再構築のT-SQLが使用される", "Link-Title1", "ボディーテスト1dddddddddddddddddddddddddddddddddddddddddddddddddSQL Server 2012]インデックス再構築タスクで、オンラインインデックス再構築できるのにオフラインインデックス再構築のT-SQLが使<h1>hello world</h1>用される");
            Article article12 = CreateArticle(category1.CategoryID, "Title2", "Link-Title2", "ボディーテスト2");
            Article article13 = CreateArticle(category1.CategoryID, "Title3", "Link-Title3", "ボディーテスト3");

            repository.AddArticle(article11);
            repository.AddArticle(article12);
            repository.AddArticle(article13);

            var articles  = Enumerable.Range(0, 20).Select(x => CreateArticle(category1.CategoryID, "PageTitle" + x.ToString(), "PageTitle-" + x.ToString(), "PageBody" + x.ToString())).ToList();
            articles.ForEach(x => repository.AddArticle(x));
            repository.SaveChanges();

            Comment comment1 = CreateComment();
            Comment comment2 = CreateComment();
            comment1.ArticleID = article11.ArticleID;
            comment2.ArticleID = article11.ArticleID;

            repository.AddComment(comment1);
            repository.AddComment(comment2);

            // お気に入りデータの作成
            FavoriteLink link1 = CreateFavoriteLink("http://www.google.co.jp", "Google");
            FavoriteLink link2 = CreateFavoriteLink("http://www.bing.com", "Bing");
            FavoriteLink link3 = CreateFavoriteLink("http://www.yahoo.co.jp", "ヤフー");
            repository.AddFavoriteLink(link1);
            repository.AddFavoriteLink(link2);
            repository.AddFavoriteLink(link3);

            repository.SaveChanges();
        }
        public static Category CreateCategory(string title, string description)
        {
            return new Category()
            {
                DisplayTitle = title,
                LID = title.Replace(" ", "-"),
                Description = description,
                ShortDescription = description,
                AddedBy = "admin",
                UpdatedBy = "admin",
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CssClass = "fa fa-cloud"
            };
        }

        public static Article CreateArticle(int categoryId, string title, string linkTitle, string body)
        {
            Article article = new Article
            {
                DisplayTitle = title,
                LID = linkTitle,
                Abstract = body,
                Body = body,
                ExpireDate = DateTime.MaxValue,
                ReleaseDate = DateTime.UtcNow,
                OnlyForMembers = false,
                Listed = true,
                CommentsEnabled = true,
                Approved = true,
                CategoryID = categoryId,
                AddedBy = "admin",
                UpdatedBy = "admin",
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            return article;
        }

        public static Comment CreateComment()
        {
            Comment comment = new Comment
            {
                AddedBy = "Test",
                Body = "Comment1",
                AddedByWeb = "http://www.yahoo.co.jp",
                UpdatedBy = "admin",
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            return comment;
        }

        public static FavoriteLink CreateFavoriteLink(string url, string text)
        {
            FavoriteLink link = new FavoriteLink
            {
                Url = url,
                Text = text,
                AddedBy = "admin",
                UpdatedBy = "admin",
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            return link;
        }
    }
}