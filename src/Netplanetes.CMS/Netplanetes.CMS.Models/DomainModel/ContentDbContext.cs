using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.DomainModel
{
    public class ContentDbContext : DbContext
    {
        #region constructor
        public ContentDbContext() { }
        public ContentDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
        #endregion

        #region properties
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FavoriteLink> FavoriteLinks { get; set; }
        #endregion

        #region methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().Property(x => x.LID)
            //    .HasColumnAnnotation(System.Data.Entity.Infrastructure.Annotations.IndexAnnotation.AnnotationName,
            //    new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute("IX_CategoryLID") { IsUnique = true })
            //    );
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
