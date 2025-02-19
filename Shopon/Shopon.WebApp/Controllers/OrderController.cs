using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopon.Business.Contracts;
using Shopon.WebApp.Models.Util;
using Shopon.WebApp.Models;
using System.Security.Claims;
using Shopon.Common.Models;

namespace Shopon.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderManager orderManager;

        public OrderController(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        [Authorize]
        public IActionResult PlaceOrder()
        {
            //1. Create order
            //2. Get/Create Customer -AppNetUsers/Customer
            //3. Get OrderItems - cartDetials - Session
            var cartVMs = HttpContext.Session.GetSession<List<CartVM>>("cartData");
            if(cartVMs == null || cartVMs.Count ==0)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = new Order
            {
                Customer = new Customer
                {
                    ASPNetUserId = user
                },
                OrderDate = DateTime.Now,
                OrderStatus = "New",
            };

            foreach(var cartVM in cartVMs)
            {
                var orderitem = new OrderItem
                {
                    Product = new Product
                    {
                        Name = cartVM.Name,
                        Id = cartVM.Id,
                        Price = cartVM.Price,
                        ImageUrl = cartVM.ImageUrl,
                    },
                    Qty = cartVM.Qty,
                };
                order.AddOrderItem(orderitem);
            }

            var neworder = orderManager.AddOrder(order);
            return View(neworder);
        }
    }
}
