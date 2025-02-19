using Microsoft.EntityFrameworkCore;
using Shopon.Common.Models;
using Shopon.Data.Contracts;
using Shopon.EF.Models;

namespace Shopon.EF
{
    /// <summary>
    /// The ProductEFRepository
    /// </summary>
    public class ProductEFRepository : IProductRepository
    {
        private readonly DbShoponContext context;

        public ProductEFRepository(DbShoponContext context)
        {
            this.context = context;
        }
        public void AddProduct(Common.Models.Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Common.Models.Product> GetPaginatedProducts(int page, int productsPerPage)
        {
            throw new NotImplementedException();
        }

        public Common.Models.Product? GetProductById(int id)
        {
            //Common.Models.Product? product = null;
            //foreach (var dbproduct in context.Products.Include(c => c.Company))
            //{
            //    if (dbproduct.ProductId == id)
            //    {
            //        product = new Common.Models.Product()
            //        {
            //            Id = dbproduct.ProductId,
            //            Name = dbproduct.ProductName!,
            //            ImageUrl = dbproduct.ImageUrl!,
            //            AvailableStatus = dbproduct.Availablestatus == "Y" ? true : false,
            //            Price = Convert.ToDouble(dbproduct.Price),
            //            Company = new Common.Models.Company
            //            {
            //                CompanyName = dbproduct.Company!.CompanyName!,
            //            }
            //        };

            //        //return products;

            //    }

            //}
            //return product;

            var dbproduct = context.Products
                .AsNoTracking()
                .Where(x => x.IsDeleted == false && x.Company.IsDeleted == false)
                .Include(x => x.Company)
                .FirstOrDefault(x => x.ProductId == id);
            if (dbproduct == null)
            {
                throw new Exception("The product not found");
            }
            Common.Models.Product product = null;
            try
            {
                product = new Common.Models.Product()
                {
                    Id = dbproduct.ProductId,
                    Name = dbproduct.ProductName!,
                    ImageUrl = dbproduct.ImageUrl!,
                    AvailableStatus = dbproduct.Availablestatus == "Y" ? true : false,
                    Price = Convert.ToDouble(dbproduct.Price),
                    Company = new Common.Models.Company()
                    {
                        CompanyName = dbproduct.Company!.CompanyName!,
                    }
                };
            }
            catch (Exception)
            {

                throw;
            }
            return product;
        }

        public IEnumerable<Common.Models.Product> GetProducts() // Fully Qualified Name
        {
            List<Common.Models.Product> products = new List<Common.Models.Product>();
            try
            {
                //var dbProducts = context.Products.ToList(); // does early loading //Type(1)
                var dbProducts = context.Products
                            .Where(x => x.IsDeleted == false && x.Company.IsDeleted == false)  // This where returns iQuerable == Checks the isdelectd in the backend
                            .Include(c => c.Company);

                // Here we are checking for IsDelected in both company an products
                /*
                    SELECT CASE
                    WHEN EXISTS (
                        SELECT 1
                            FROM [products] AS [p]
                            LEFT JOIN [companies] AS [c] ON [p].[company_id] = [c].[company_id]
                            WHERE [p].[isDeleted] = CAST(0 AS bit) AND [c].[isDeleted] = CAST(0 AS bit)) THEN CAST(1 AS bit)
                            ELSE CAST(0 AS bit)
                            END 
                 */
                if (dbProducts.Any())
                {
                    // ================================ Lambda Expression also used in delegate ==========================
                    products = dbProducts
                                .Where(x => x.IsDeleted == false)
                                .Select(x => new Common.Models.Product
                                {
                                    Id = x.ProductId,
                                    Name = x.ProductName,
                                    ImageUrl = x.ImageUrl,
                                    AvailableStatus = x.Availablestatus == "Y" ? true : false,
                                    Price = Convert.ToDouble(x.Price),
                                    Company = new Common.Models.Company
                                    {
                                        CompanyName = x.Company.CompanyName,
                                    }
                                }).ToList();

                    /*
                    // ============================= LINQ =================================================================
                    products = (from dbProduct in dbProducts
                                where dbProduct.IsDeleted == false
                                select new Common.Models.Product
                                {
                                    Id = dbProduct.ProductId,
                                    Name = dbProduct.ProductName!,
                                    ImageUrl = dbProduct.ImageUrl!,
                                    AvailableStatus = dbProduct.Availablestatus == "Y" ? true : false,
                                    Price = dbProduct.Price!.Value,
                                    Company = new Common.Models.Company
                                    {
                                        CompanyName = dbProduct.Company!.CompanyName!,
                                    }
                                }).ToList();  // No Need of writing of for each loop and display.

                    */

                    //=========================================Normal Looping access========================================
                    //foreach(var dbProduct in dbProducts)
                    //{
                    //    Common.Models.Product product = new Common.Models.Product()
                    //    {
                    //        Id = dbProduct.ProductId,
                    //        Name = dbProduct.ProductName,
                    //        ImageUrl = dbProduct.ImageUrl,
                    //        AvailableStatus = dbProduct.Availablestatus == "Y" ? true : false,
                    //        Price = dbProduct.Price!.Value,
                    //        Company = new Common.Models.Company()
                    //        {
                    //            //CompanyName = "Apple", //Type(1)
                    //            CompanyName = dbProduct.Company!.CompanyName!
                    //        }
                    //    };
                    //    products.Add(product);
                    //}
                }

            } catch (Exception ex)
            {
                // log
                throw;
            }
            return products;
        }

        public int GetTotalProductCount()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Common.Models.Product>? SearchProducts(string searchey)
        {
            List<Common.Models.Product> products = new List<Common.Models.Product> ();

            foreach (var dbProduct in context.Products.Where(x => x.ProductName.Contains(searchey!)).Include(x => x.Company))
            {
                Common.Models.Product product = new Common.Models.Product
                {
                    Id = dbProduct.ProductId,
                    Name = dbProduct.ProductName,
                    ImageUrl = dbProduct.ImageUrl,
                    AvailableStatus = dbProduct.Availablestatus == "Y" ? true : false,
                    Price = dbProduct.Price!.Value,
                    Company = new Common.Models.Company()
                    {
                        CompanyName = dbProduct.Company!.CompanyName!
                    }
                };
                products.Add(product);
            }


            return products;
        }
    }
}
