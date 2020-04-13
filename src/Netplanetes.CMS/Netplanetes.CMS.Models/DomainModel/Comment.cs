using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.DomainModel
{
    [Table("Comment",Schema="dbo")]
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        [Required]
        [StringLength(1024)]
        public string Body { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime AddedDate { get; set; }
        [Required]
        [StringLength(256)]
        public string AddedBy { get; set; }
        [StringLength(256)]
        public string AddedByEmail { get; set; }
        [StringLength(40)]
        [Column(TypeName = "nvarchar")]
        public string AddedByIP { get; set; }
        [StringLength(1024)]
        public string AddedByWeb { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime UpdatedDate { get; set; }
        [Required]
        [StringLength(256)]
        public string UpdatedBy { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int ArticleID { get; set; }
        [ForeignKey("ArticleID")]
        public Article Article { get; set; }
    }
}
