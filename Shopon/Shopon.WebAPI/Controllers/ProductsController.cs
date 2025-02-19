using Microsoft.AspNetCore.Mvc;
using Shopon.Business.Contracts;

namespace Shopon.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager productManager;

        public ProductsController(IProductManager productManager)
        {
            this.productManager = productManager;
        }

        // GET: /api/products
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = productManager.GetProducts();
                return Ok(products);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching product details. Contact admin.");
            }
        }

        // GET: /api/products/1
        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var products = productManager.GetProducts();
                var product = products.FirstOrDefault(x => x.Id == id);
                if (product == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Product Not Found!");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching product details. Contact admin.");
            }
        }
    }
}