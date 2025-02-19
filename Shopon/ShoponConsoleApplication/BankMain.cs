using Shopon.Business;
using Shopon.Business.Contracts;
using Shopon.Common.Models;
using ShoponConsoleApplication.Utils;

namespace ShoponConsoleApplication
{
    public class BankMain
    {
        public void Main(IBankManager bankManager)
        {
            int choice = 0;
            do
            {
                Console.WriteLine("Product Menu");
                Draw.DrawLine("=", 30);
                Console.WriteLine("1. Add Bank");
                Console.WriteLine("2. Get Bank Detials");
                Console.WriteLine("3. Back");
                Console.WriteLine("0. Exit");
                Console.WriteLine("Enter Your Choice: ");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddBankData(bankManager);
                        break;
                    case 2:
                        GetBankDetials(bankManager);
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

            } while (choice != 0);

        }

        private void GetBankDetials(IBankManager bankManager)
        {
            try
            {
                var banks = bankManager.GetAllBanks();
                if (banks == null)
                {
                    Console.WriteLine("No banks found!");
                }
                else
                {
                    foreach (var bank in banks)
                    {
                        Console.WriteLine($"{bank.BankId} \t {bank.BankName}");
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void AddBankData(IBankManager bankManager)
        {
            try
            {
                //Offer offer1 = new Offer()
                //{
                //    Discount = 10,
                //    OfferTime = DateTime.Now,
                //    OfferType = "ICICI CC offer on Christmus",
                //    Remark = "Get 10% Offer on Christmas"
                //};
                //Offer offer2 = new Offer()
                //{
                //    Discount = 7,
                //    OfferTime = DateTime.Now,
                //    OfferType = "ICICI DC offer on Christmus",
                //    Remark = "Get 7% Offer on Christmas"
                //};

                //Bank bank = new Bank()
                //{
                //    BankName = "ICICI Bank",
                //    Branch = "MG Road",
                //    City = "Banglore",
                //    State = "Karnataka",
                //    IFSC = "ICICI00045",
                //    IsDeleted = false,
                //};
                Offer offer1 = new Offer
                {
                    Discount = 5,
                    OfferTime = DateTime.Now,
                    OfferType = "HSBC CC Offer",
                    Remark = "Get 5% Offer on Christmas"
                };
                Offer offer2 = new Offer
                {
                    Discount = 10,
                    OfferTime = DateTime.Now,
                    OfferType = "HSBC DC Offer",
                    Remark = "Get 10% Offer on Christmas"
                };
                Bank bank = new Bank
                {
                    BankName = "HSBC Bank",
                    Branch = "M.G.Road",
                    City = "Bangalore",
                    State = "Karnataka",
                    IFSC = "HSBC00049",
                    IsDeleted = false,
                };

                bank.Offers.Add(offer1);
                bank.Offers.Add(offer2);

                var newBank = bankManager.AddBank(bank);

                Console.WriteLine($"Your bank id is {newBank.BankId}");

            } catch(Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
            }
        }
    }
}
