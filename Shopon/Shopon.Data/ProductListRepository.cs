using Shopon.Common.CustomException;
using Shopon.Common.Models;
using Shopon.Data.Contracts;


namespace Shopon.Data
{
    /// <summary>
    /// The ProductListRepository
    /// </summary>
    public class ProductListRepository : IProductRepository, IProductAdminRepository
    {
        private List<Product> products= new List<Product>();


        private readonly ICompanyRepository companyRepository;

        public ProductListRepository(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public void AddProduct(Product product)
        {
            this.products.Add(product);
        }

        public IEnumerable<Product> GetProducts() // IEnumerable Makes the code more maintainable 
        {
            foreach (var product in this.products)
            {
                var company = companyRepository.GetCompanyById(product.Company.CompanyId);
                if (company != null)
                {
                    product.Company = company;
                }
            }
            return products;
        }

        public Product? GetProductById(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new InvalidProductException("No Such Product Found");
            }
            return product;
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
