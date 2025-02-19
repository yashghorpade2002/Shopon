using Shopon.Business.Contracts;
using Shopon.Common.Models;
using ShoponConsoleApplication.Utils;

namespace ShoponConsoleApplication
{
    public class CompanyMain
    {
        public void Main(ICompanyManager companyManager)
        {
            int choice = 0;
            do
            {
                Console.WriteLine("Company Menu");
                Draw.DrawLine("=", 30);
                Console.WriteLine("1. Add Company");
                Console.WriteLine("2. Display Companies");
                Console.WriteLine("3. Get Company By Id");
                Console.WriteLine("4. Back");
                Console.WriteLine("Enter Your Choice: ");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddCompany(companyManager);
                        break;
                    case 2:
                        DisplayCompanies(companyManager);
                        break;
                    case 3:
                        GetCompanyById(companyManager);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

            } while (choice != 0);




        }

        private void GetCompanyById(ICompanyManager companyManager)
        {
            Console.WriteLine("Enter company Id");
            int id = Convert.ToInt16(Console.ReadLine());

            var company = companyManager.GetCompanyById(id);
            if (company == null)
            {
                Console.WriteLine("No Company found");
                return;
            }
            Console.WriteLine("CompanyId \t Company Name");
            Draw.DrawLine("=", 30);
            DisplayCompany(company);

        }

        private void DisplayCompanies(ICompanyManager companyManager)
        {
            var companies = companyManager.GetCompanies();
            if(companies == null)
            {
                Console.WriteLine("No Companies found");
                return;
            }
            Console.WriteLine("CompanyId \t Company Name");
            Draw.DrawLine("-", 60);
            foreach (var company in companies)
            {
                DisplayCompany(company);
            }
            Draw.DrawLine("-", 60);
        }

        private void AddCompany(ICompanyManager companyManager)
        {
            Company company1 = new Company
            {
                CompanyId = 1016,
                CompanyName = "Nothing"
            };

            var company = companyManager.AddCompany(company1);
            if (company != null)
            {
                Console.WriteLine($"Company added successfully");
            } else
            {
                Console.WriteLine($"Company was not added ");
            }
        }

        private void DisplayCompany(Company company)
        {
            Console.WriteLine($"{company.CompanyId}\t\t{company.CompanyName}");
        }

    }
}
