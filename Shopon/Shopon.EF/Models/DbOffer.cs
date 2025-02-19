using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopon.EF.Models
{
    [Table("Offers")]
    internal class DbOffer
    {
        [Key]
        [Column("OfferId")]
        public int OfferId { get; set; }

        [Column("OfferType")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string OfferType { get; set; }

        [Column("Discount")]
        public int Discount { get; set; }

        [Column("Remark")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string Remark { get; set; }

        [Column("OfferTime")]
        public DateTime OfferTime { get; set; } = DateTime.Now;

        [Column("BankId")]
        public int BankId { get; set; } //Navigation Property ==> Forigen key
        public DbBank Bank { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

    }
}
