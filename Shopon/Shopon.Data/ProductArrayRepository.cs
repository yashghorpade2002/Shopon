using Shopon.Common.Models;
using Shopon.Data.Contracts;

namespace Shopon.Data
{
    public class ProductArrayRepository : IProductRepository, IProductAdminRepository
    {
        private Product[] products;
        private int capacity = 4;
        private int index;

        //Constructor overloading for the stake of flexibility
        public ProductArrayRepository(int capacity)
        {
            products = new Product[capacity];
            this.capacity = capacity;
        }
        public ProductArrayRepository()
        {
            products = new Product[capacity];
        }

        public void AddProduct(Product product)
        {
            //this.product = product ?? throw new ArgumentNullException("Product is empty");

            if (product == null)
            {
                throw new ArgumentNullException("Product is empty");
            }
            if(index >= capacity)  // This is our array list we dont have array list in .net but we have list and we do have array list in java
            {
                Product[] temp = new Product[capacity];
                Array.Copy(products, temp, products.Length);
                products = new Product[capacity*2];
                Array.Copy(temp, products, temp.Length);
                capacity = capacity*2;

            }
            products[index] = product;
            index++;
            
        }

        public IEnumerable<Product> GetProducts() 
        {
            Product[] activeProducts = null;  // Here we are creating our own collection
            Array.Copy (this.products, activeProducts, index);
            return activeProducts; 
        }

        public Product? GetProductById(int id)
        {
            
            return products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product>? SearchProducts(string searchey)
        {
            return products;
        }

        public IEnumerable<Product> GetPaginatedProducts(int page, int productsPerPage)
        {
            throw new NotImplementedException();
        }

        public int GetTotalProductCount()
        {
            throw new NotImplementedException();
        }
    }
}
