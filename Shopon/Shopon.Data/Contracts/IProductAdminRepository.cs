using Shopon.Common.Models;

namespace Shopon.Data.Contracts
{
    /// <summary>
    /// The IProductAdminRepository
    /// </summary>
    public interface IProductAdminRepository : IProductRepository
    {

        /// <summary>
        /// Method to add Product
        /// </summary>
        /// <param name="product"></param>
        void AddProduct(Product product);
    }
}
