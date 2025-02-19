using Shopon.Common.Models;

namespace Shopon.Business.Util
{
    public class PriceComparer : IComparer<Product>
    {
        public int Compare(Product? x, Product? y)
        {
            if(x == null && y == null) return 1;
            if(x.Price > y.Price) return -1;
            else if(x.Price < y.Price) return 1;
            return 0;
        }
    }
}
