using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Shopon.Business;
using Shopon.Business.Contracts;
using Shopon.Common.Models;
using Shopon.WebAPI.Models;

namespace Shopon.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAsynController : ControllerBase
    {
        private readonly IProductAsyncManager productManager;
        private readonly ICompanyAsyncManager companyManager;

        public ProductAsynController(IProductAsyncManager productManager, ICompanyAsyncManager companyManager)
        {
            this.productManager = productManager;
            this.companyManager = companyManager;
        }

        // POST: /api/productasync
        [HttpPost]
        public async Task<IActionResult> AddProducts(ProductVM productVM)
        {
            try
            {
                // Check for product
                if (productVM == null)
                {
                    return BadRequest();
                }

                // validate Company
                var company = await companyManager.GetCompanyByIdAsync(productVM.Company.CompanyId);
                if (company == null)
                {
                    return NotFound("Company not Found");
                }
                if (productVM.Company == null || productVM.Company.CompanyId == 0)
                {
                    return NotFound("No Company Found");
                }
                var product = new Product
                {
                    AvailableStatus = productVM.AvailableStatus,
                    Company = new Company
                    {
                        CompanyId = productVM.Company.CompanyId,
                    },
                    ImageUrl = productVM.ImageUrl,
                    Name = productVM.ProductName,
                    Price = productVM.Price,
                };
                var newProduct = await productManager.AddProductAsync(product);

                //1. status Code 201
                //2. Newly created object
                //3. Location in header of newaly created object
                return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While adding product contact admin");
            }
        }

        //GET: api/productAsync/1
        [HttpGet("{id:int}")]
        //[Route("Product")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await productManager.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound("No Product Found");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                // log
                throw;
            }
        }

        //PUT: /api/updateProducts/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int productId, Product productVM)
        {
            //1. product is null
            //2. check for id and product.id
            try
            {
                // Check for product
                if (productVM == null)
                {
                    return BadRequest();
                }
                if(productVM.Id != productId)
                {
                    return BadRequest($"Product Id Mismatch");
                }

                // validate Company
                var company = await companyManager.GetCompanyByIdAsync(productVM.Company.CompanyId);
                if (company == null)
                {
                    return NotFound("Company not Found");
                }
                if (productVM.Company == null || productVM.Company.CompanyId == 0)
                {
                    return NotFound("No Company Found");
                }
                var product = new Product
                {
                    AvailableStatus = productVM.AvailableStatus,
                    Company = new Company
                    {
                        CompanyId = productVM.Company.CompanyId,
                    },
                    ImageUrl = productVM.ImageUrl,
                    Name = productVM.Name,
                    Price = productVM.Price,
                };
                var newProduct = await productManager.UpdateProductAsync(product);

                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While updating product contact admin");
            }
        }

        //DELETE: /api/productasync/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            try
            {
                var product = await productManager.DeleteProductAsync(id);
                if(product == null)
                {
                    return BadRequest("No Product Found");
                }
                return Ok(product);
            } catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While deleting product contact admin");
            }
        }

        //GET: /api/SearchProducts
        [HttpGet("SearchProducts")]
        public async Task<IActionResult> SearchProducts(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return BadRequest("Search key not found!");
                }

                var products = await productManager.SearchProductsAsync(key);
                if (products == null)
                {
                    return NotFound("No products found!");
                }
                
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching products");
            }
        }



        //GET: /api/productsAsync
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await productManager.GetAllProductsAsync();
                if (products == null)
                {
                    return NotFound($"No Product found!");
                }
                ProductVM productVM = new ProductVM()
                {

                };
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while fetching product details. Contact admin.");
            }
        }
    }
}
