using Shopon.Business.Contracts;
using Shopon.Business;
using Shopon.Data.Contracts;
using Shopon.Data;
using ShoponConsoleApplication.Utils;
using Shopon.ADO;
using Shopon.EF;
using Shopon.EF.Models;

namespace ShoponConsoleApplication
{
    public class ShoponMain
    {
        public void MainMenu()
        {
            ICompanyRepository companyRepository = new CompanyADORepository();

            //ICompanyRepository companyRepository = new CompanyListRepository();
            ICompanyManager companyManager = new CompanyManager(companyRepository); // DI


            //IProductRepository productRepository = new ProductListRepository(companyRepository);
            IProductRepository productRepository = new ProductADORepository();
            //IProductRepository productRepository = new ProductEFRepository();
            IProductManager productManager = new ProductManager(productRepository); // DI

            ICustomerRepository customerRepository = new CustomerADORepository();
            ICustomerManager customerManager = new CustomerManager(customerRepository); //DI

            IOrderRepository orderRepository = new OrderADORepository();
            IOrderManager orderManager = new OrderManager(orderRepository);

            
            //IBankRepository bankRepository = new BankEFRepository();
            //IBankManager bankManager = new BankManager(bankRepository);

            ProductMain productMain = new ProductMain();
            CompanyMain companyMain = new CompanyMain();
            OrderMain orderMain = new OrderMain();
            BankMain bankMain = new BankMain();


            int choice = 0;
            do
            {
                Console.WriteLine("Product Menu");
                Draw.DrawLine("=", 30);
                Console.WriteLine("1. Product Menu");
                Console.WriteLine("2. Company Menu");
                Console.WriteLine("3. Order Menu");
                Console.WriteLine("4. Bank Menu");
                Console.WriteLine("0. Exit");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        productMain.Main(productManager);
                        break;
                    case 2:
                        companyMain.Main(companyManager);
                        break;
                    case 3:
                        orderMain.Main(orderManager);
                        break;
                    case 4:
                        //bankMain.Main(bankManager);
                        break;
                    case 5:
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

            } while (choice != 0);
        }
    }
}
