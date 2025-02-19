using Shopon.Common.Models;

namespace Shopon.Data.Contracts
{
    /// <summary>
    /// The IProductRepository
    /// </summary>
    public interface IProductRepository
    {

        /// <summary>
        /// Method to get all products
        /// </summary>
        /// <returns>List of Products</returns>
        public IEnumerable<Product> GetProducts();

        /// <summary>
        /// Method to get product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product if found</returns>
        public Product? GetProductById(int id);

        /// <summary>
        /// Method to get all products
        /// </summary>
        /// <returns>List of Products</returns>
        public IEnumerable<Product>? SearchProducts(string searchey);

        /// <summary>
        /// Method to get products paginated
        /// </summary>
        /// <param name="page"></param>
        /// <param name="productsPerPage"></param>
        /// <returns>set of products per page only</returns>
        public IEnumerable<Product> GetPaginatedProducts(int page, int productsPerPage);

        /// <summary>
        /// Method to calculate total number of products in the database
        /// </summary>
        /// <returns>count of products</returns>
        public int GetTotalProductCount();

    }



    //public interface IProductAdminRepository
    //{
    //    void AddProduct(Product product);
    //}
}
