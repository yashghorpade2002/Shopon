using Shopon.Business;
using Shopon.Business.Contracts;
using Shopon.Common.CustomException;
using Shopon.Common.Models;
using Shopon.Data;
using Shopon.Data.Contracts;
using ShoponConsoleApplication.Utils;

namespace ShoponConsoleApplication
{
    /// <summary>
    /// The ProductMain
    /// </summary>
    public class ProductMain
    {
        public void Main(IProductManager productManager)
        {
            //int pid = 0;
            //string pname = string.Empty;
            //double price = 0;
            //Product product = new Product();

            

            int choice = 0;
            do
            {
                Console.WriteLine("Product Menu");
                Draw.DrawLine("=", 30);
                Console.WriteLine("1. Add Products");
                Console.WriteLine("2. Display Products");
                Console.WriteLine("3. Sort by Id");
                Console.WriteLine("4. Sort by Price");
                Console.WriteLine("5. Get Product By ID");
                Console.WriteLine("6. Search Products");
                Console.WriteLine("6. Back");
                Console.WriteLine("0. Exit");
                Console.WriteLine("Enter Your Choice: ");
                choice = Convert.ToInt16(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        AddProductData(productManager);
                        break;
                    case 2:
                        DisplayProductData(productManager);
                        break;
                    case 3:
                        SortById(productManager);
                        break;
                    case 4:
                        SortByPrice(productManager);
                        break;
                    case 5:
                        GetProductById(productManager);
                        break;
                    case 6:
                        SearchProducts(productManager);
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

            } while (choice != 0);

            
            
            
        }

        private void SearchProducts(IProductManager productManager)
        {
            try
            {
                string key = string.Empty;
                Console.WriteLine("Enter the search Key");
                key = Console.ReadLine();
                var products = productManager.SearchProducts(key);
                //if(string.IsNullOrEmpty(key))
                //{
                //    Console.WriteLine("No Key Passed");
                //    return;
                //}
                if(products.Count() <= 0)
                {
                    Console.WriteLine("No products found");
                    return;
                }
                Console.WriteLine($"PID \t Name \t\t\t Price \t\t Company \t\t ImageUrl");
                Draw.DrawLine("=", 30);
                foreach (var product in products)
                {
                    {
                        DisplayProducts(product);
                    }
                }
                Draw.DrawLine("=", 30);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        private void GetProductById(IProductManager productManager)
        {
            int id;
            try
            {
                Console.WriteLine("Enter Id");
                id = Convert.ToInt16(Console.ReadLine());
                var product = productManager.GetProductById(id);
                //if(product == null) // We are checking for the null ness of the object in productListRepository by throwing custom exception
                //{
                //    Console.WriteLine("No Product Found !");
                //    return;
                //}
                Console.WriteLine($"PID \t Name \t\t\t Price \t\t Company \t\t ImageUrl");
                Draw.DrawLine("=", 30);
                DisplayProducts(product);
                Console.WriteLine($"{product.Ratings.ratings} \t\t {product.Ratings.FkProductId} \t\t {product.Categories.category_Name}");
                Draw.DrawLine("=", 30);

            }
            catch(InvalidProductException ie)
            {
                Console.WriteLine($"Error Message: {ie}");
                Console.WriteLine($"Root Message: {ie.InnerException!.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
            }
        }

        private void SortByPrice(IProductManager productManager)
        {
            var sortedProduct = productManager.GetProductPriceSorted();
            if(sortedProduct == null)
            {
                Console.WriteLine("No Product Found");
            }
            else
            {
                Console.WriteLine($"PID \t Name \t\t\t Price \t\t Company \t\t ImageUrl");
                Draw.DrawLine("=", 30);
                foreach (var product in sortedProduct)
                {
                    DisplayProducts(product);
                }
                Draw.DrawLine("=", 30);
            }
        }

        private void SortById(IProductManager productManager)
        {
            var sortedProduct = productManager.GetProductSorted();
            if (sortedProduct == null)
            {
                Console.WriteLine("No Product Found");
            }
            else
            {
                Console.WriteLine($"PID \t Name \t\t\t Price \t\t Company \t\t ImageUrl");
                Draw.DrawLine("=", 30);
                foreach (var product in sortedProduct)
                {
                    //if (product != null)
                    {
                        DisplayProducts(product);
                    }
                }
                Draw.DrawLine("=", 30);
            }
        }

        private void DisplayProductData(IProductManager productManager)
        {
            //Console.WriteLine("Product ID = " + pid + " Product Name = " + pname + " Price = " + price);
            //Console.WriteLine("Product ID = {0} Product Name = {1}  Price = {2}", pid, pname, price);
            //Console.WriteLine($"Product ID = {pid} Product Name = {pname}  Price = {price}");

            try
            {
                var products = productManager.GetProducts();
                if (products == null)
                {
                    Console.WriteLine("No Product Found");
                }
                else
                {
                    Console.WriteLine($"PID \t Name \t\t\t Price \t\t  Company \t\t ImageUrl");
                    Draw.DrawLine("=", 30);
                    foreach (var product in products)
                    {
                        //if (product != null)
                        {
                            DisplayProducts(product);
                        }
                    }
                    Draw.DrawLine("=", 30);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"The Error: {ex.Message}");
            }
        }

        private void DisplayProducts(Product product)
        {
            Console.WriteLine($"{product.Id}\t" +
                $"{product.Name}\t\t" +
                $"{product.Price}\t\t" +
                $"{product.Company.CompanyName}\t\t" +
                $"{product.ImageUrl}");
        }

        private void AddProductData(IProductManager productManager)
        {
            Product product1 = new Product
            {
                Id = 1,
                Name = "Ipad Mini",
                AvailableStatus = true,
                ImageUrl = "/imagaes/apple/1.jpg",
                Price = 150000,
                Company = new Company
                {
                    CompanyId = 1,
                }

            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "Iphone 6s",
                AvailableStatus = true,
                ImageUrl = "/imagaes/apple/1.jpg",
                Price = 88000,
                Company = new Company
                {
                    CompanyId = 1,
                }
            };
            Product product3 = new Product
            {
                Id = 3,
                Name = "Iphone 6",
                AvailableStatus = true,
                ImageUrl = "/imagaes/apple/1.jpg",
                Price = 54000,
                Company = new Company
                {
                    CompanyId = 1,
                }

            };
            Product product4 = new Product
            {
                Id = 4,
                Name = "Iphone 7s",
                AvailableStatus = true,
                ImageUrl = "/imagaes/apple/1.jpg",
                Price = 67000,
                Company = new Company
                {
                    CompanyId = 1,
                }

            };
            Product product5 = new Product
            {
                Id = 5,
                Name = "Iphone 7",
                AvailableStatus = true,
                ImageUrl = "/imagaes/apple/1.jpg",
                Price = 56000,
                Company = new Company
                {
                    CompanyId = 1,
                }

            };
            Product product6 = new Product
            {
                Id = 6,
                Name = "Iphone 12",
                AvailableStatus = true,
                ImageUrl = "/imagaes/apple/1.jpg",
                Price = 44000,
                Company = new Company
                {
                    CompanyId = 1,
                }

            };
            Product product7 = new Product
            {
                Id = 7,
                Name = "Samsung S22",
                AvailableStatus = true,
                ImageUrl = "/imagaes/samsung/1.jpg",
                Price = 140000,
                Company = new Company
                {
                    CompanyId = 2,
                }

            };
            Product product8 = new Product
            {
                Id = 8,
                Name = "Samsung S23",
                AvailableStatus = true,
                ImageUrl = "/imagaes/samsung/1.jpg",
                Price = 130000,
                Company = new Company
                {
                    CompanyId = 2,
                }

            };
            Product product9 = new Product
            {
                Id = 9,
                Name = "Samsung A14",
                AvailableStatus = true,
                ImageUrl = "/imagaes/samsung/1.jpg",
                Price = 35000,
                Company = new Company
                {
                    CompanyId = 2,
                }

            };
            Product product10 = new Product
            {
                Id = 10,
                Name = "Samsung S24",
                AvailableStatus = true,
                ImageUrl = "/imagaes/samsung/1.jpg",
                Price = 125000,
                Company = new Company
                {
                    CompanyId = 2,
                }

            };

            /*Console.WriteLine("Enter product id:");
            product.Id = CustomConverter.GetInt(Console.ReadLine());

            Console.WriteLine("Enter product name:");
            product.Name = Console.ReadLine();

            Console.WriteLine("Enter price:");
            product.Price = CustomConverter.GetDouble(Console.ReadLine());

            Console.WriteLine("Enter Image URL:");
            product.ImageUrl = Console.ReadLine();
            */

            /*
            productManager.AddProduct(product1);
            productManager.AddProduct(product10);
            productManager.AddProduct(product5);
            productManager.AddProduct(product3);
            productManager.AddProduct(product2);
            productManager.AddProduct(product8);
            productManager.AddProduct(product7);
            productManager.AddProduct(product4);
            productManager.AddProduct(product6);
            productManager.AddProduct(product9);
            */

            //var products = productManager.GetProducts();
            //foreach( var product in products )
            //{
            //    if (product.Company.CompanyId == 1)
            //    {
            //        product.Company.CompanyName = "Apple";
            //    }
            //    else
            //    {
            //        product.Company.CompanyName = "Samsung";
            //    }
            //}
        }
    }
}
