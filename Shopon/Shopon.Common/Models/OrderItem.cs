namespace Shopon.Common.Models
{
    /// <summary>
    /// The OrderItem
    /// </summary>
    public class OrderItem
    {
        public int OrderId { get; set; }

        public Product Product { get; set; }
        public int Qty { get; set; }
        public double Amount
        {
            get
            {
                return Product.Price * Qty;
            }
        }


    }
}
