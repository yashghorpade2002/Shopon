namespace Shopon.Common.Models
{
    public class Offer
    {
        public int OfferId { get; set; }
        public string OfferType { get; set; }
        public int Discount { get; set; }
        public string Remark { get; set; }
        public DateTime OfferTime { get; set; } = DateTime.Now;
        //public int BankId { get; set; } //Navigation Property ==> Forigen key
        public Bank Bank { get; set; }
        public bool IsDeleted { get; set; }
    }
}
