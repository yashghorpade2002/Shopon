using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopon.EF.Models
{
    [Table("Banks")]
    internal class DbBank
    {
        [Key]
        [Column("BankId")]
        public int BankId { get; set; }

        [Column("BankName")]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string? BankName { get; set; }

        [Column("Branch")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string? Branch { get; set; }

        [Column("City")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string? City { get; set; }

        [Column("State")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string? State { get; set; }

        [Column("IFSC")]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        public string? IFSC { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        public virtual ICollection<DbOffer> Offers { get; set; }

        public void AddOffer(DbOffer offer) => this.Offers.Add(offer);

        public DbBank()
        {
            Offers = new List<DbOffer>();
        }

    }
}
