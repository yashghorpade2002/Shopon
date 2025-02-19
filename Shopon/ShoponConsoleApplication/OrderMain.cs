using Shopon.Business;
using Shopon.Business.Contracts;
using Shopon.Common.Models;
using ShoponConsoleApplication.Utils;

namespace ShoponConsoleApplication
{
    public class OrderMain
    {
        public void Main(IOrderManager orderManager)
        {
            int choice = 0;
            do
            {
                Console.WriteLine("Company Menu");
                Draw.DrawLine("=", 30);
                Console.WriteLine("1. Add Order");
                Console.WriteLine("2. Back");
                Console.WriteLine("Enter Your Choice: ");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddOrder(orderManager);
                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

            } while (choice != 0);
        }

        private void AddOrder(IOrderManager orderManager)
        {
            try
            {
                var product1 = new Product()
                {
                    Id = 1,
                    Name = "Apple I-Pad Mini",
                    Price = 25699,
                    AvailableStatus = true,
                    ImageUrl = "images/Apple/01.jpg"
                };
                var product2 = new Product()
                {
                    Id = 5,
                    Name = "Apple I-Phone 6s",
                    Price = 50000,
                    AvailableStatus = true,
                    ImageUrl = "images/Apple/05.jpg"
                };

                var orderItem1 = new OrderItem()
                {
                    Product = product1,
                    Qty = 1
                };
                var orderItem2 = new OrderItem()
                {
                    Product = product2,
                    Qty = 1
                };

                Customer customer = new Customer()
                {
                    CustomerId = 1,
                    CustomerName = "Amruthi",
                    MobileNumber = "9988665544",
                    EmailId = "amruthi@gmail.com"
                };

                Order order = new Order()
                {
                    Customer = customer,
                    OrderDate = DateTime.Now,
                    OrderStatus = "New"
                };

                order.AddOrderItem(orderItem1);
                order.AddOrderItem(orderItem2);

                var newOrder = orderManager.AddOrder(order);
                if(newOrder != null)
                {
                    Console.WriteLine($"Your order Placed. {newOrder.OrderId} is your order id");
                } else
                {
                    Console.WriteLine("Your order is not placed contact admin");
                }

            } catch (Exception e)
            {
                //log
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
