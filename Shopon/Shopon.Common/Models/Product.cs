namespace Shopon.Common.Models
{
    public class Product: IComparable<Product>
    {
        public int Id { get; set; } // select pro.product_id  AS Id
        public string Name { get; set; }
        public double Price { get; set; }
        public bool AvailableStatus { get; set; }
        public string ImageUrl { get; set; }
        public Company Company { get; set; }

        public ProductRatings? Ratings { get; set; }

        public Categories? Categories { get; set; }

        public int CompareTo(Product? other)
        {
            if(other == null) return 1;
            if(this.Id < other.Id) return -1;
            else if(this.Id > other.Id) return 1;
            else return 0;
        }
    }
}
