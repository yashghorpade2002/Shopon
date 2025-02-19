using Shopon.Common.Models;

namespace Shopon.Business.Contracts
{
    /// <summary>
    /// The IProductAsyncManager
    /// </summary>
    public interface IProductAsyncManager
    {
        /// <summary>
        /// Method to add new product async
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Newely added products</returns>
        public Task<Product> AddProductAsync(Product product);

        /// <summary>
        /// Method to get all products async
        /// </summary>
        /// <returns>List Of Products</returns>
        public Task<IEnumerable<Product>> GetAllProductsAsync();

        /// <summary>
        /// Method to get the product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product by if found else returns null</returns>
        public Task<Product?> GetProductByIdAsync(int id);

        /// <summary>
        /// Method to update the product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Updated product</returns>
        public Task<Product?> UpdateProductAsync(Product product);

        /// <summary>
        /// Method to delete the product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted product if found else returns null</returns>
        public Task<Product?> DeleteProductAsync(int id);

        /// <summary>
        /// Method to search the products
        /// </summary>
        /// <param name="search"></param>
        /// <returns>returns the list of products found else returns null</returns>
        public Task<IEnumerable<Product>?> SearchProductsAsync(string search);
    }
}
