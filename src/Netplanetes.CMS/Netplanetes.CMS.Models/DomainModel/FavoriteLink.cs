using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.DomainModel
{
    [Table("FavoriteLink", Schema="dbo")]
    public class FavoriteLink
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavoriteLinkID { get; set; }
        [Required]
        [StringLength(1024)]
        public string  Url { get; set; }
        [Required]
        [StringLength(256)]
        public string Text { get; set; }
        [Range(0, 1000)]
        public int SortOrder { get; set; }
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

    }
}
