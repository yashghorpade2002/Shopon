using Shopon.Common.Models;

namespace Shopon.Business.Contracts
{
    public interface IProductManager
    {
        /*
        /// <summary>
        /// Method to add products
        /// </summary>
        /// <param name="product"></param>
        void AddProduct(Product product);

        */
        /// <summary>
        /// Mathod to Get the Products
        /// </summary>
        /// <returns>Returns List of Products</returns>
        public IEnumerable<Product> GetProducts();

        /// <summary>
        /// Mathod To Get products Sorted
        /// </summary>
        /// <returns>Returns list of products sorted by Id</returns>
        public IEnumerable<Product> GetProductSorted();

        /// <summary>
        /// Mathos to get the products price sorted
        /// </summary>
        /// <returns>Returns list of products sorted by price</returns>
        public IEnumerable<Product> GetProductPriceSorted();

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
}
