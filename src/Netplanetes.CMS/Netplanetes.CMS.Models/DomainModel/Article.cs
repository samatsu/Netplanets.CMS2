using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.DomainModel
{
    /// <summary>
    /// ページを表すクラス
    /// </summary>
    [Table("Article", Schema="dbo")]
    public class Article
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleID { get; set; }
        [Required]
        [StringLength(256)]
        public string DisplayTitle { get; set; }
        [Required]
        [StringLength(256)]
        [IndexAttribute("IX_ArticleLID", IsUnique = true)]
        public string LID { get; set; }
        [Required]
        [StringLength(256)]
        public string Abstract { get; set; }
        [Required]
        public string Body { get; set; }
        /// <summary>
        /// 複数の記事をグルーピングするシリーズタグ
        /// </summary>
        [MaxLength(20)]
        [IndexAttribute("IX_ArticleSeries")]
        public string Series { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        [IndexAttribute("IX_CategoryIDReleaseDate", Order = 2)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime ExpireDate { get; set; }
        [Required]
        public bool Approved { get; set; }
        [Required]
        public bool Listed { get; set; }
        [Required]
        public bool CommentsEnabled { get; set; }
        [Required]
        public bool OnlyForMembers { get; set; }
        [Required]
        public int ViewCount { get; set; }
        [Required]
        public int Votes { get; set; }
        [Required]
        public int TotalRating { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime AddedDate { get; set; }
        [Required]
        [StringLength(256)]
        public string AddedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime UpdatedDate { get; set; }
        [Required]
        [StringLength(256)]
        public string UpdatedBy { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [IndexAttribute("IX_CategoryIDReleaseDate", Order=1)]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}
