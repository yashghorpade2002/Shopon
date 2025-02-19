namespace Shopon.WebApp.Models
{
    public class CartVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public double Price { get; set; }

        public int Qty { get; set; }

        public double Amount { get { return Qty * Price; } }
    }
}
