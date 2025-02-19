using Microsoft.AspNetCore.Mvc;
using Shopon.Business.Contracts;
using Shopon.WebApp.Models;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Shopon.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductManager productManager;

        public HomeController(ILogger<HomeController> logger, IProductManager productManager)
        {
            _logger = logger;
            this.productManager = productManager;
        }

        //public IActionResult Index()
        //{
        //    var products = productManager.GetProducts();
        //    return View(products);
        //}

        public IActionResult Index(int page = 1, int productsPerPage = 20)
        {
            var products = productManager.GetPaginatedProducts(page, productsPerPage);

            // Get total count for pagination controls
            var totalProducts = productManager.GetTotalProductCount();
            var totalPages = (int)Math.Ceiling((double)totalProducts / productsPerPage);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(products);
        }

        [HttpPost]
        public IActionResult Index(string key)
        {
            var products = productManager.SearchProducts(key);
            return View(products);


        }
        public IActionResult Details(int pid)
        {
            try
            {
                var products = productManager.GetProductById(pid);
                return View(products);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
