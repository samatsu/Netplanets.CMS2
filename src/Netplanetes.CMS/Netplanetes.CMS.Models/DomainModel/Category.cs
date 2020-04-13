using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.DomainModel
{
    [Table("Category", Schema = "dbo")]
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [Required]
        [StringLength(256)]
        public string DisplayTitle { get; set; }
        [Required]
        [StringLength(256)]
        [IndexAttribute("IX_CategoryLID", IsUnique=true)]
        public string LID { get; set; }
        [Required]
        [Range(0, 1000)]
        public int SortOrder { get; set; }
        [Required]
        [StringLength(32)]
        public string ShortDescription { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(256)]
        public string CssClass { get; set; }

        [Required]
        [Column(TypeName="datetime2")]
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

        public virtual ICollection<Article> Articles { get; set; }

    }
}
