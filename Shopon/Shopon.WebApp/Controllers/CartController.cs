using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopon.Business.Contracts;
using Shopon.WebApp.Models;
using Shopon.WebApp.Models.Util;

namespace Shopon.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductManager productManager;

        public CartController(IProductManager productManager)
        {
            this.productManager = productManager;
        }
        public IActionResult AddToCart(int pid)
        {
            try
            {
                var product = productManager.GetProductById(pid);
                var cartVM = new CartVM()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Qty = 1
                };
                //TempData["cartData"]= JsonConvert.SerializeObject(cartVM);
                //1.Check if session is present
                //2.We read data / add new cartVM to session
                //3 Create new list of cartvm
                //3. Add list to session
                var cartVMs = HttpContext.Session.GetSession<List<CartVM>>("cartData");
                if (cartVMs == null)
                {
                    cartVMs = new List<CartVM>();
                    cartVMs.Add(cartVM);

                }
                else
                {
                    var existingProduct = cartVMs.FirstOrDefault(x => x.Id == cartVM.Id);
                    if (existingProduct != null)
                    {
                        if (existingProduct.Qty >= 5)
                        {
                            TempData["Error"] = "Quantity cannot exceed 5 for this product.";
                        }
                        else
                        {
                            existingProduct.Qty++;
                        }
                    }
                    else
                    {
                        cartVMs.Add(cartVM);
                    }
                }

                HttpContext.Session.SetSession<List<CartVM>>("cartData", cartVMs);

            }
            catch (Exception ex)
            {
                throw;
            }
            return RedirectToAction("ViewCartDetails");
        }

        public IActionResult ViewCartDetails(int id)
        {
            var cartVMs = HttpContext.Session.GetSession<List<CartVM>>("cartData");
            if (cartVMs != null)
            {
                var cartCnt = cartVMs.Count();
                HttpContext.Session.SetInt32("CartCnt", cartCnt);
            }

            return View(cartVMs);
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cartVMs = HttpContext.Session.GetSession<List<CartVM>>("cartData");
            var itemToDelete = cartVMs.FirstOrDefault(x => x.Id == id);
            if (itemToDelete != null)
            {
                cartVMs.Remove(itemToDelete);
                HttpContext.Session.SetSession<List<CartVM>>("cartData", cartVMs);
            }
            return RedirectToAction("ViewCartDetails");
        }

        [HttpPost]
        public IActionResult UpdateCartQuantity(int id, int qty)
        {

            var cartVMs = HttpContext.Session.GetSession<List<CartVM>>("cartData");


            var cartItem = cartVMs?.FirstOrDefault(x => x.Id == id);
            if (cartItem != null)
            {

                if (qty < 1) qty = 1;
                if (qty > 5) qty = 5;

                cartItem.Qty = qty;


                HttpContext.Session.SetSession<List<CartVM>>("cartData", cartVMs);
            }

            return Ok();
        }
    }
}
