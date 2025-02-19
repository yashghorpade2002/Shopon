using Shopon.Business.Contracts;
using Shopon.Business.Util;
using Shopon.Common.CustomException;
using Shopon.Common.Models; 
using Shopon.Data.Contracts;

namespace Shopon.Business
{
    public class ProductManager : IProductManager
    {
        private IProductRepository productRepository;
        //private ProductArrayRepository productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            //this.productRepository = new ProductListRepository();
            //this.productRepository = new ProductArrayRepository();
            this.productRepository = productRepository;
        }

        //public void AddProduct(Product product)
        //{
        //    this.productRepository.AddProduct(product);
        //}

        public IEnumerable<Product> GetProducts()
        {
           return this.productRepository.GetProducts();
        }

        public IEnumerable<Product> GetProductSorted()
        {
            var products = productRepository.GetProducts();
            var list = products.ToList();
            list.Sort();
            return list;
        }

        public IEnumerable<Product> GetProductPriceSorted()
        {
            var products = productRepository.GetProducts();
            var list = products.ToList();
            list.Sort(new PriceComparer());
            return list;
        }

        public Product? GetProductById(int id)
        {
            try
            {
                return this.productRepository.GetProductById(id);
            }
            catch (InvalidProductException pe)
            {
                throw new InvalidProductException("Méiyǒu zhǎodào cǐ lèi chǎnpǐn", pe);
            }
        }

        public IEnumerable<Product>? SearchProducts(string searchey)
        {
            return this.productRepository.SearchProducts(searchey);
        }

        public IEnumerable<Product> GetPaginatedProducts(int page, int productsPerPage) => this.productRepository.GetPaginatedProducts(page, productsPerPage);

        public int GetTotalProductCount() => this.productRepository.GetTotalProductCount();
    }
}
