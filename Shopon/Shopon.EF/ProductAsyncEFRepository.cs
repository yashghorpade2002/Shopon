using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shopon.Data.Contracts;
using Shopon.EF.Models;
using System.Net.Http.Headers;

namespace Shopon.EF
{
    public class ProductAsyncEFRepository : IProductAsyncRepository
    {
        private readonly DbShoponContext context;

        public ProductAsyncEFRepository(DbShoponContext context)
        {
            this.context = context;
        }
        public async Task<Common.Models.Product> AddProductAsync(Common.Models.Product product)
        {
            try
            {
                var dbProduct = new Models.Product();
                MapToDB(product, dbProduct);
                var result = await context.Products.AddAsync(dbProduct);
                await context.SaveChangesAsync();
                product.Id = result.Entity.ProductId;
                return await GetProductByIdAsync(result.Entity.ProductId);
            } catch (Exception ex)
            {
                // log error
                throw;
            }
        }

        public async Task<Common.Models.Product?> DeleteProductAsync(int id)
        {
            try
            {
                var dbProduct = await context.Products.Where(x => x.IsDeleted == false && x.ProductId == id).FirstOrDefaultAsync();

                if(dbProduct != null)
                {
                    dbProduct.IsDeleted = true;
                    await context.SaveChangesAsync();
                    var product = new Common.Models.Product();
                    MapToEntity(dbProduct, product);

                    return product;
                }
            } catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public async Task<IEnumerable<Common.Models.Product>> GetAllProductsAsync()
        {
            try
            {
                var dbProducts = await context.Products
                                    .AsNoTracking()
                                    .Include(x => x.Company)
                                    .Include(x => x.Category)
                                    .Where(x => x.IsDeleted == false)
                                    .ToListAsync();
                if(dbProducts != null)
                {
                    return GetProducts(dbProducts);
                }

            } catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public async Task<Common.Models.Product?> GetProductByIdAsync(int id)
        {
            try
            {
                var dbProduct =  await context.Products
                                .AsNoTracking()
                                .Include(x => x.Company)
                                .Include(x => x.Category)
                                .Where(x => x.ProductId == id && x.IsDeleted == false)
                                .FirstOrDefaultAsync();
                Common.Models.Product product = new Common.Models.Product();
                MapToEntity(dbProduct, product);
                return product;
            } catch (Exception ex)
            {
                //log
                throw;
            }
        }

        public async Task<IEnumerable<Common.Models.Product>?> SearchProductsAsync(string search)
        {
            try
            {
                var dbProducts = await context.Products
                    .Include(x => x.Company)
                    .Include(x => x.Category)
                    .Where(x => x.IsDeleted == false && x.ProductName!.Contains(search))
                    .ToListAsync();
                if(dbProducts != null)
                {
                    return GetProducts(dbProducts);
                }
            } catch (Exception ex)
            {
                // log
                throw;
            }
            return null;
        }

        public async Task<Common.Models.Product?> UpdateProductAsync(Common.Models.Product product)
        {
            try
            {
                var dbProduct = await context.Products.Where(x => x.IsDeleted == false && x.ProductId == product.Id).FirstOrDefaultAsync();

                if(dbProduct != null)
                {
                    MapToDB(product, dbProduct);
                    await context.SaveChangesAsync();

                    return await GetProductByIdAsync(product.Id);
                }
            } catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        #region Private methods
        private IEnumerable<Common.Models.Product>? GetProducts(List<Models.Product> dbProducts)
        {
            List<Common.Models.Product> products = new List<Common.Models.Product>();
            foreach (var dbProduct in dbProducts)
            {
                var product = new Common.Models.Product();
                MapToEntity(dbProduct, product);
                products.Add(product);
            }
            return products;
        }
        private void MapToDB(Common.Models.Product product, Models.Product dbProduct)
        {
            dbProduct.ProductName = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.Availablestatus = product.AvailableStatus == true ? "Y" : "N";
            dbProduct.ImageUrl = product.ImageUrl;
            dbProduct.CompanyId = product.Company.CompanyId;
        }

        private void MapToEntity(Models.Product dbProduct, Common.Models.Product product)
        {
            product.Id = dbProduct.ProductId;
            product.Name = dbProduct.ProductName!;
            product.Price = dbProduct.Price!.Value;
            product.AvailableStatus = dbProduct.Availablestatus == "Y" ? true : false;
            product.ImageUrl = dbProduct.ImageUrl!;
            product.Company = new Common.Models.Company
            {
                CompanyId = dbProduct.Company!.CompanyId,
                CompanyName = dbProduct.Company!.CompanyName!
            };
            product.Categories = new Common.Models.Categories
            {
                CategoryId = dbProduct.Category!.CategoryId,
                category_Name = dbProduct.Category!.CategoryName!
            };
        }

        #endregion
    }
}
