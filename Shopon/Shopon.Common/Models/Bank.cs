using System;

namespace Shopon.Common.Models
{
    /// <summary>
    /// The Bank
    /// </summary>
    public class Bank
    {
        public int BankId { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? IFSC { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Offer> Offers { get; set; }
        public void AddOffer(Offer offer) => this.Offers.Add(offer);

        public Bank()
        {
            Offers = new List<Offer>();
        }
    }
}
